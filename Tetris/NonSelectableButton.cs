using System.Windows.Forms;

namespace Tetris
{
    class NonSelectableButton : Button
    {
        public NonSelectableButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
