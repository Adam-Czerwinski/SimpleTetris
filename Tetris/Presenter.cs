using System;
using Tetris.Model;

namespace Tetris
{
    class Presenter
    {
        IView _view;
        IGameEngine _gameEngine;

        public Presenter(IView view, IGameEngine gameEngine)
        {
            _view = view;
            _gameEngine = gameEngine;

            //przypisanie akcji przycisku startGame
            _view.StartGame += RunEngine;
            _view.unPause += unPauseEngine;

            //Przypisanie strzałek (A,D) do metod znajdujących się w silniku gry
            _view.UpArrow += _gameEngine.RotateTetromino;
            _view.LeftArrow += _gameEngine.MoveLeft;
            _view.RightArrow += _gameEngine.MoveRight;
            _view.Spacebar += _gameEngine.FallDown;
            //Timer elapsed to kolejny ruch co upływ czasu, więc po prostu spadek w dół
            _view.DownArrow += _gameEngine.Timer_Elapsed;

            _gameEngine.Timer.Elapsed += Update;
            _gameEngine.Changes += RequestRedraw;
            _gameEngine.AddScore += IncreaseScore;
        }

        /// <summary>
        /// Metoda wywołująca się kiedy gracz wstrzyma grę lub chce ją kontynuować
        /// </summary>
        private void unPauseEngine()
        {
            _gameEngine.unPause();
            if (GameEngine.State == GameState.PAUSED)
                _view.PausedLabelVisibility(true);
            else if(GameEngine.State==GameState.PLAYING)
                _view.PausedLabelVisibility(false);
        }

        /// <summary>
        /// Metoda wywołująca się kiedy wystąpi akcja z GameEngine "AddScore"
        /// Dostarcza nowy wynik 
        /// </summary>
        private void IncreaseScore()
        {
            //Ustaw wynik z innego wątku
            _view.SetScore(_gameEngine.Player.Score);
        }

        /// <summary>
        /// Metoda wywołująca się kiedy wystąpi akcja z GameEngine "ChangesInBoard"
        /// Dostarcza nową tablicę 
        /// </summary>
        private void RequestRedraw()
        {
            //Przekazanie nowych danych
            _view.Board = _gameEngine.TetrisBoard.Board;
        }

        /// <summary>
        /// Metoda się wywołuje co interwał
        /// </summary>
        private void Update(object sender, EventArgs e)
        {
            //Dopóki jest w grze
            if (GameEngine.State == GameState.PLAYING)
                //Aktualizuj logicznie
                _gameEngine.Timer_Elapsed();
            else if(GameEngine.State == GameState.NOT_PLAYING)
                StopEngine();
        }

        /// <summary>
        /// Startuje silnik
        /// </summary>
        private void RunEngine()
        {
            //Ustaw, żeby przycisku nie było widać
            _view.StartButtonVisibility(false);
            //Ustaw wynik początkowy, czyli 0
            _view.SetScore(0);

            // L E C I M Y!!!!!
            _gameEngine.Run();

            //Dzięki temu nie trzeba czekać na wywołanie metody Update przez timer określonego interwału
            Update(null, null);
        }

        /// <summary>
        /// Zatrzymuje silnik
        /// </summary>
        private void StopEngine()
        {
            _gameEngine.Stop();
            _view.ShowMessage("Koniec gry! Twój wynik to " + _gameEngine.Player.Score.ToString());
            _view.StartButtonVisibility(true);
        }

    }
}
