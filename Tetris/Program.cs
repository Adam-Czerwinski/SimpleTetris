using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Model;

namespace Tetris
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Wysokość i szerokość tablicy gry.
            int width = 16;
            int height = 22;
            TetrisBoard tetrisBoard = new TetrisBoard(width, height);

            //Stworzenie wszystkich tetromino
            Tetromino[] tetrominos = new Tetromino[] {
                new Tetromino(TetrominoType.I, Color.Red),
                new Tetromino(TetrominoType.T, Color.SaddleBrown),
                new Tetromino(TetrominoType.O, Color.DarkGoldenrod),
                new Tetromino(TetrominoType.L, Color.Green),
                new Tetromino(TetrominoType.J, Color.Blue),
                new Tetromino(TetrominoType.S, Color.Brown),
                new Tetromino(TetrominoType.Z, Color.DarkCyan)
            };

            //utworzenie gracza
            Player player = new Player();
            //utworzenei silnika
            IGameEngine gameEngine = new GameEngine(tetrisBoard, player, tetrominos, 500);

            //utworzenie widoku
            IView view = new Form();

            //utworzenie presentera
            Presenter presenter = new Presenter(view, gameEngine);

            Application.Run((Form)view);
        }
    }
}
