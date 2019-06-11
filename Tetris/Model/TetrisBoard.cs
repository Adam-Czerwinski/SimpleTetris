using System.Collections.Generic;

namespace Tetris.Model
{
    class TetrisBoard
    {
        /// <summary>
        /// Tablica reprezentująca tablicę gry jako dwuwymiarowa tablica.
        /// Elementy w teblicy wyglądają następująco: 
        ///     {0,7}
        ///     {1,3}
        ///     {2,5}
        ///     {3,1}
        ///     {4,2}
        /// Pierwszy wymiar tablicy określa wysokość
        /// Drugi wymiar tablicy określa szerokość
        /// Dodatkowo, wartość int określa kolor bloku
        /// </summary>
        public int[,] Board { get; set; }

        /// <summary>
        /// Wymiary są reprezentowane w ilości klocków 
        /// </summary>
        /// <param name="width">Ilość klocków pionowo</param>
        /// <param name="height">Ilość klocków poziomo</param>
        public TetrisBoard(int width, int height)
        {
            //+4 height tablicy żeby ukryć Tetromino
            height += 4;
            Board = new int[height, width];
        }
    }
}
