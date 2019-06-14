using System.Windows.Forms;

namespace Tetris
{
    class TetrisPanel : Panel
    {
        public TetrisPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }
}
