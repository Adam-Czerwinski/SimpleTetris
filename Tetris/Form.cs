using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Model;

namespace Tetris
{
    public partial class Form : System.Windows.Forms.Form, IView
    {

        public Form()
        {
            InitializeComponent();
        }

        public event Action StartGame;
        public event Action LeftArrow;
        public event Action RightArrow;
        public event Action DownArrow;
        public event Action Spacebar;

        /// <summary>
        /// Tablica odzwierciedlająca grę
        /// </summary>
        private int[,] _board;
        public int[,] Board { set { _board = value; panelTetrisBoard.Invalidate(); } }

        /// <summary>
        /// Kliknięcie przycisku start
        /// </summary>
        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        /// <summary>
        /// Kliknięcie klawisza w formie
        /// </summary>
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                LeftArrow();
            else if (e.KeyCode == Keys.D)
                RightArrow();
            else if (e.KeyCode == Keys.S)
                DownArrow();
            else if (e.KeyCode == Keys.Space)
                Spacebar();
        }

        /// <summary>
        /// Dostęp z innego wątku do widoczności przycisku
        /// </summary>
        /// <param name="value"></param>
        public void StartButtonVisibility(bool value)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                buttonStartGame.Visible = value;
            }));
        }

        /// <summary>
        /// Dostęp z innego wątku do wyniki
        /// </summary>
        /// <param name="score"></param>
        public void SetScore(int score)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                labelScoreActually.Text = score.ToString();
            }));
        }

        /// <summary>
        /// Dostęp z innego wątku do MessageBoxa
        /// </summary>
        public void ShowMessage(string msg)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                MessageBox.Show(msg);
            }));
        }

        /// <summary>
        /// Redraw panel
        /// </summary>
        private void PanelTetrisBoard_Paint(object sender, PaintEventArgs e)
        {
            panelTetrisBoard.SuspendLayout();
            if (_board == null)
                return;

            Graphics tableBoard = e.Graphics;
            tableBoard.Clear(Color.DarkGray);

            #region Siatka

            Pen penGrid = new Pen(Color.AliceBlue, 0.3f);
            //Linie pionowe
            int howManyLines = _board.GetLength(1);
            for (int i = 1; i < howManyLines; i++)
            {
                tableBoard.DrawLine(penGrid, i * Square.Width, 0, i * Square.Width, _board.GetLength(0) * Square.Height);
            }

            //Linie poziome
            howManyLines = _board.GetLength(0);
            for (int i = 1; i < howManyLines; i++)
            {
                tableBoard.DrawLine(penGrid, 0, i * Square.Height, _board.GetLength(1) * Square.Width, i * Square.Width);
            }
            penGrid.Dispose();
            #endregion

            #region Rysowanie kwadratów z zewnętrznymi ramkami

            //Pen pen = null;
            //Rectangle rectangle;
            ////pozycje X oraz Y prostokątu
            //int posX, posY;

            //for (int i = 0; i < _board.GetLength(0); i++)
            //{
            //    for (int j = 0; j < _board.GetLength(1); j++)
            //    {
            //        if (_board[i, j] == 0)
            //            continue;
            //        else
            //        {
            //            posX = j * Square.Width + 1;
            //            //Zmniejszamy o 4, żeby na początku ukryło cały prostokąt
            //            posY = (i - 4) * Square.Height + 1;
            //            rectangle = new Rectangle(posX, posY, Square.Width - 2, Square.Height - 2);

            //            //Wypełnienie kolorem
            //            pen = new Pen(Color.FromArgb(_board[i, j]));
            //            //pen.Width = 2;
            //            tableBoard.DrawRectangle(pen, rectangle);
            //            pen.Dispose();
            //        }
            //    }
            //}

            #endregion

            #region Rysowanie kwadratów wypełnionych

            Rectangle rectangle;
            Brush brush;
            //pozycje X oraz Y prostokątu
            int posX, posY;

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    if (_board[i, j] == 0)
                        continue;

                    posX = j * Square.Width + 1;
                    //Zmniejszamy o 4, żeby na początku ukryło cały prostokąt
                    posY = (i - 4) * Square.Height + 1;
                    rectangle = new Rectangle(posX, posY, Square.Width - 2, Square.Height - 2);

                    //Wypełnienie kolorem
                    brush = new SolidBrush(Color.FromArgb(_board[i, j]));
                    //pen.Width = 2;
                    tableBoard.FillRectangle(brush, rectangle);
                    brush.Dispose();

                }
            }

            #endregion
            panelTetrisBoard.ResumeLayout();
        }
    }
}
