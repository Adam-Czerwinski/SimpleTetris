using System;
using System.Timers;

namespace Tetris.Model
{
    public enum GameState { PLAYING, NOT_PLAYING }
    class GameEngine : IGameEngine
    {
        /// <summary>
        /// Akcja wywołująca kiedy zajdą zmiany w tablicy odzwierciedlającej logikę gry (TetrisBoard.Board)
        /// </summary>
        public event Action Changes;
        /// <summary>
        /// Akcja wywołująca kiedy zajdą zmiany w wyniku gracza (Player.Score)
        /// </summary>
        public event Action AddScore;

        public Player Player { get; set; }
        public TetrisBoard TetrisBoard { get; set; }
        public Tetromino[] Tetrominos { get; set; }
        public Timer Timer { get; set; }
        public static GameState State { get; set; } = GameState.NOT_PLAYING;


        public GameEngine(TetrisBoard tetrisBoard, Player player, Tetromino[] tetrominos, int interval)
        {
            TetrisBoard = tetrisBoard;
            Player = player;
            Tetrominos = tetrominos;

            Timer = new Timer();
            Timer.Stop();
            Timer.Interval = interval;

            NewTetromino();
        }

        /// <summary>
        /// Co upływ czasu aktualne tetromino opada w dół.
        /// Zauważ, że Timer nie jest podpięty do tej metody.
        /// Ta metoda jest wykonywana manualnie!
        /// </summary>
        public void Timer_Elapsed()
        {
            //bo za każdym razem tetromino spada
            MoveDown();

            //Wyczyść linie (o ile jest co)
            ClearLines();
        }

        /// <summary>
        /// Nagłe opadniecie w dół jak najdalej może
        /// </summary>
        public void FallDown()
        {
            //Zapobiega to rozpoczęciu gry w jakiś dziwny sposób
            //jeżeli nie jest w grze to nic nie rób
            if (State == GameState.NOT_PLAYING)
                return;

            int[][] newPosition = new int[4][];

            //Głęboka kopia. Przepisz aktualną pozycję tetromino do nowych pozycji
            for (int i = 0; i < Player.Tetromino.Position.GetLength(0); i++)
            {
                newPosition[i] = new int[2];
                for (int j = 0; j < 2; j++)
                    newPosition[i][j] = Player.Tetromino.Position[i][j];
            }

            //ile kratek w dół może opaść
            int ile = 0;

            //Wykonuj cały czas dopóki nie będzie mógł znaleźć nowej pozycji
            while (true)
            {
                for (int i = 0; i < Player.Tetromino.Position.GetLength(0); i++)
                {
                    int x = newPosition[i][0] + 1;
                    int y = newPosition[i][1];
                    newPosition[i] = new int[] { x, y };
                }

                //Jeżeli znajdziesz pozycję w której nie może się znaleźć to przerwij pętlę
                if (!CanMoveDown(newPosition))
                {
                    //Zmniejsz o jedną kratkę, ponieważ na górze aktualizowaliśmy odrazu newPosition
                    for (int i = 0; i < Player.Tetromino.Position.GetLength(0); i++)
                        newPosition[i][0] -= 1;

                    break;
                }

                //w przeciwnym wypadku zwiększ o ile może ruszyć się
                ile++;
            }

            //Jeżeli może spaść chociaż o jedną pozycję w dól to spadnij
            if (ile > 0)
                UpdateTetrisBoard(newPosition);
        }

        /// <summary>
        /// Spadanie w dół
        /// </summary>
        private void MoveDown()
        {
            //Zapobiega to rozpoczęciu gry w jakiś dziwny sposób
            //jeżeli nie jest w grze to nic nie rób
            if (State == GameState.NOT_PLAYING)
                return;

            int[][] newPosition = new int[4][];
            for (int i = 0; i < Player.Tetromino.Position.GetLength(0); i++)
            {
                int x = Player.Tetromino.Position[i][0] + 1;
                int y = Player.Tetromino.Position[i][1];
                newPosition[i] = new int[] { x, y };
            }

            if (CanMoveDown(newPosition))
                UpdateTetrisBoard(newPosition);
            else
                NewTetromino();
        }

        /// <summary>
        /// Sprawdza czy tetromino może spaść w dół
        /// </summary>
        private bool CanMoveDown(int[][] newPosition)
        {
            //Głęboka kopia aby móc wyczyścić aktualną pozycję
            int[,] temporaryTetrisBoard = new int[TetrisBoard.Board.GetLength(0), TetrisBoard.Board.GetLength(1)];
            for (int i = 0; i < TetrisBoard.Board.GetLength(0); i++)
                for (int j = 0; j < TetrisBoard.Board.GetLength(1); j++)
                    temporaryTetrisBoard[i, j] = TetrisBoard.Board[i, j];

            //Czyszczenie aktualnej pozycji w tymczasowej tablicy
            foreach (int[] p in Player.Tetromino.Position)
                temporaryTetrisBoard[p[0], p[1]] = 0;

            bool canChangePosition = true;
            bool isGameOver = false;
            foreach (int[] np in newPosition)
            {
                //jeżeli indeks nowej pozycji pokrywa się z długością pierwszego wymiaru (czyli wysokość) to wychodzi poza tablicę
                if (np[0] == temporaryTetrisBoard.GetLength(0))
                {
                    canChangePosition = false;
                    break;
                }
                //jeżeli nowa pozycja wskazuje już na jakiś klocek
                else if (temporaryTetrisBoard[np[0], np[1]] != 0)
                {
                    canChangePosition = false;

                    //Jeżeli nowa pozycja nawet nie wyjdzie z ukrycia
                    foreach (int[] np2 in newPosition)
                        if (np2[0] == 4)
                            isGameOver = true;

                    break;
                }
            }

            if (isGameOver)
            {
                GameEngine.State = GameState.NOT_PLAYING;
            }

            return canChangePosition;
        }

        /// <summary>
        /// Porusz aktualny tetromino w lewo
        /// </summary>
        public void MoveLeft()
        {
            //Zapobiega to rozpoczęciu gry w jakiś dziwny sposób
            //jeżeli nie jest w grze to nic nie rób
            if (State == GameState.NOT_PLAYING)
                return;

            int[][] newPosition = new int[4][];
            for (int i = 0; i < Player.Tetromino.Position.GetLength(0); i++)
            {
                int x = Player.Tetromino.Position[i][0];
                int y = Player.Tetromino.Position[i][1] - 1;
                newPosition[i] = new int[] { x, y };
            }

            if (CanMoveLeft(newPosition))
                UpdateTetrisBoard(newPosition);
        }

        /// <summary>
        /// Sprawdza czy tetromino może poruszyć się w lewo
        /// </summary>
        private bool CanMoveLeft(int[][] newPosition)
        {
            bool canMoveLeft = true;

            //---------------SPRAWDZENIE LEWEJ KRAWĘDZI-------------------
            foreach (int[] np in newPosition)
            {
                //jeżeli wychodzy poza lewą krawędź
                if (np[1] < 0)
                {
                    canMoveLeft = false;
                    return canMoveLeft;
                }
            }

            //---------------SPRAWDZENIE CZY Z LEWEJ STRONY JEST JUŻ KLOCEK-------------------
            int[,] temporaryTetrisBoard = new int[TetrisBoard.Board.GetLength(0), TetrisBoard.Board.GetLength(1)];
            for (int i = 0; i < TetrisBoard.Board.GetLength(0); i++)
                for (int j = 0; j < TetrisBoard.Board.GetLength(1); j++)
                    temporaryTetrisBoard[i, j] = TetrisBoard.Board[i, j];

            //Czyszczenie aktualnej pozycji w tymczasowej tablicy
            foreach (int[] p in Player.Tetromino.Position)
                temporaryTetrisBoard[p[0], p[1]] = 0;

            foreach (int[] np in newPosition)
            {
                //jeżeli wychodzy poza lewą krawędź
                if (temporaryTetrisBoard[np[0], np[1]] != 0)
                {
                    canMoveLeft = false;
                    break;
                }
            }

            return canMoveLeft;
        }

        /// <summary>
        /// Porusz aktualny tetromino w prawo
        /// </summary>
        public void MoveRight()
        {
            //Zapobiega to rozpoczęciu gry w jakiś dziwny sposób
            //jeżeli nie jest w grze to nic nie rób
            if (State == GameState.NOT_PLAYING)
                return;

            int[][] newPosition = new int[4][];
            for (int i = 0; i < Player.Tetromino.Position.GetLength(0); i++)
            {
                int x = Player.Tetromino.Position[i][0];
                int y = Player.Tetromino.Position[i][1] + 1;
                newPosition[i] = new int[] { x, y };
            }


            if (CanMoveRight(newPosition))
                UpdateTetrisBoard(newPosition);

        }

        /// <summary>
        /// Sprawdza czy tetromino może poruszyć się w prawo
        /// </summary>
        private bool CanMoveRight(int[][] newPosition)
        {
            bool canMoveRight = true;

            //---------------SPRAWDZENIE PRAWEJ KRAWĘDZI-------------------
            foreach (int[] np in newPosition)
            {
                //jeżeli wychodzy poza prawą krawędź
                if (np[1] == TetrisBoard.Board.GetLength(1))
                {
                    canMoveRight = false;
                    return canMoveRight;
                }
            }

            //---------------SPRAWDZENIE CZY Z LEWEJ STRONY JEST JUŻ KLOCEK-------------------
            int[,] temporaryTetrisBoard = new int[TetrisBoard.Board.GetLength(0), TetrisBoard.Board.GetLength(1)];
            for (int i = 0; i < TetrisBoard.Board.GetLength(0); i++)
                for (int j = 0; j < TetrisBoard.Board.GetLength(1); j++)
                    temporaryTetrisBoard[i, j] = TetrisBoard.Board[i, j];

            //Czyszczenie aktualnej pozycji w tymczasowej tablicy
            foreach (int[] p in Player.Tetromino.Position)
                temporaryTetrisBoard[p[0], p[1]] = 0;

            foreach (int[] np in newPosition)
            {
                //jeżeli wychodzy poza lewą krawędź
                if (temporaryTetrisBoard[np[0], np[1]] != 0)
                {
                    canMoveRight = false;
                    break;
                }
            }

            return canMoveRight;
        }

        /// <summary>
        /// Aktualizuje tablicę gry usuwając starą pozycję aktualnego tetromino i wpisując nową
        /// </summary>
        /// <param name="newPosition">nowa pozycja tetromino</param>
        private void UpdateTetrisBoard(int[][] newPosition)
        {
            //Wyczyść tablicę z aktualnej pozycji
            foreach (int[] p in Player.Tetromino.Position)
                TetrisBoard.Board[p[0], p[1]] = 0;

            //Przypisz nowe pozycje tetromino
            Player.Tetromino.Position = newPosition;

            foreach (int[] p in Player.Tetromino.Position)
                TetrisBoard.Board[p[0], p[1]] = Player.Tetromino.Color.ToArgb();

            Changes();
        }

        /// <summary>
        /// Usuwa linie
        /// </summary>
        private void ClearLines()
        {
            int clearedLines = 0;

            //sprawdzanie każdego wiersza czy jest uzupełniony
            bool isCompleted;
            //zaczynamy od dołu
            for (int i = TetrisBoard.Board.GetLength(0) - 1; i >= 0; i--)
            {
                isCompleted = true;
                for (int j = 0; j < TetrisBoard.Board.GetLength(1); j++)
                {
                    if (TetrisBoard.Board[i, j] == 0)
                    {
                        isCompleted = false;
                        break;
                    }
                }

                //jeżeli linia jest wypełniona
                if (isCompleted)
                {
                    clearedLines++;
                    for (int k = i; k > 0; k--)
                    {
                        for (int j = 0; j < TetrisBoard.Board.GetLength(1); j++)
                        {
                            TetrisBoard.Board[k, j] = TetrisBoard.Board[k - 1, j];
                        }
                    }

                    i--;
                }
            }

            if(clearedLines>0)
                IncreaseScore(clearedLines);
        }

        /// <summary>
        /// Dodaje punkty po wyczyszczeniu linii
        /// </summary>
        /// <param name="clearedLines">Ilość wyczyszczonych linii za jednym razem</param>
        private void IncreaseScore(int clearedLines)
        {
            Player.Score += Convert.ToInt32((clearedLines * 10) * 1.5);
            AddScore();
        }

        /// <summary>
        /// Wystartuj silnik
        /// </summary>
        public void Run()
        {
            //Timer wywołuję podpiętą metodą caly czas co interwał, a nie tylko raz (false)
            Timer.AutoReset = true;
            //Wystartuj Timer
            Timer.Start();

            //Status - w trakcie gry
            State = GameState.PLAYING;
        }

        /// <summary>
        /// Zatrzymuje silnik gry
        /// </summary>
        public void Stop()
        {
            Timer.Stop();

            GameEngine.State = GameState.NOT_PLAYING;

            //wyzeruj tablicę
            for (int i = 0; i < TetrisBoard.Board.GetLength(0); i++)
                for (int j = 0; j < TetrisBoard.Board.GetLength(1); j++)
                    TetrisBoard.Board[i, j] = 0;
        }

        /// <summary>
        /// Ustawia pozycje początkowe wszystkich tetrominosów
        /// </summary>
        private void SetTetrominoPosition(Tetromino t)
        {
            switch (t.Type)
            {
                case TetrominoType.I:
                    t.Position[0] = new int[] { 0, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[1] = new int[] { 1, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[2] = new int[] { 2, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[3] = new int[] { 3, TetrisBoard.Board.GetLength(1) / 2 };
                    break;
                case TetrominoType.J:
                    t.Position[0] = new int[] { 1, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[1] = new int[] { 2, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[2] = new int[] { 3, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[3] = new int[] { 3, (TetrisBoard.Board.GetLength(1) / 2) - 1 };
                    break;
                case TetrominoType.L:
                    t.Position[0] = new int[] { 1, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[1] = new int[] { 2, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[2] = new int[] { 3, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[3] = new int[] { 3, (TetrisBoard.Board.GetLength(1) / 2) + 1 };
                    break;
                case TetrominoType.O:
                    t.Position[0] = new int[] { 2, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[1] = new int[] { 2, (TetrisBoard.Board.GetLength(1) / 2) + 1 };
                    t.Position[2] = new int[] { 3, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[3] = new int[] { 3, (TetrisBoard.Board.GetLength(1) / 2) + 1 };
                    break;
                case TetrominoType.S:
                    t.Position[0] = new int[] { 3, (TetrisBoard.Board.GetLength(1) / 2) - 1 };
                    t.Position[1] = new int[] { 3, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[2] = new int[] { 2, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[3] = new int[] { 2, (TetrisBoard.Board.GetLength(1) / 2) + 1 };
                    break;
                case TetrominoType.T:
                    t.Position[0] = new int[] { 2, (TetrisBoard.Board.GetLength(1) / 2) - 1 };
                    t.Position[1] = new int[] { 2, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[2] = new int[] { 2, (TetrisBoard.Board.GetLength(1) / 2) + 1 };
                    t.Position[3] = new int[] { 3, TetrisBoard.Board.GetLength(1) / 2 };
                    break;
                case TetrominoType.Z:
                    t.Position[0] = new int[] { 2, (TetrisBoard.Board.GetLength(1) / 2) - 1 };
                    t.Position[1] = new int[] { 2, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[2] = new int[] { 3, TetrisBoard.Board.GetLength(1) / 2 };
                    t.Position[3] = new int[] { 3, (TetrisBoard.Board.GetLength(1) / 2) + 1 };
                    break;

            }
        }

        /// <summary>
        /// przygotowuje nowe tetromino
        /// </summary>
        private void NewTetromino()
        {
            Random random = new Random();
            int r = random.Next(0, Tetrominos.Length - 1);
            Player.Tetromino = new Tetromino(Tetrominos[r].Type, Tetrominos[r].Color);

            //ustawienie pozycji początkowej Tetromino
            SetTetrominoPosition(Player.Tetromino);
        }

    }
}