using System.Drawing;
using System.Windows.Forms;

namespace Tetris.Model
{
    public enum TetrominoType
    {
        I, T, O, L, J, S, Z
    }

    public enum TetrominoRatationMode
    {
        First, Second, Third, Fourth
    }

    class Tetromino
    {
        
        /// <summary>
        /// Typ tetromino
        /// </summary>
        public TetrominoType Type { get; set; }
        /// <summary>
        /// Typ rotacji tetromino
        /// </summary>
        public TetrominoRatationMode Rotation { get; set; }

        /// <summary>
        /// Pozycja tetromino na tablicy tetrisBoard.
        /// Pierwszy wymiar to jest klocek.
        /// Drugi wymiar to koordynaty X,Y więc długość drugiego wymiaru to 2!
        /// </summary>
        public int[][] Position { get; set; }

        /// <summary>
        /// kolor tetromino
        /// </summary>
        public Color Color { get; set; }

        /// <param name="tetrominoBlock">Dwuwymiarowa tablica 4x4 reprezentująca kwadraty za pomocą true/false</param>
        /// <param name="type">Typ tetromino</param>
        public Tetromino(TetrominoType type, Color color)
        {
            Type = type;
            Color = color;
            Rotation = TetrominoRatationMode.First;

            Position = new int[4][];
        }

    }
}
