using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    interface IView
    {
        int[,] Board { set; }
        event Action StartGame;
        void SetScore(int score);
        void StartButtonVisibility(bool value);
        void ShowMessage(string msg);
        event Action LeftArrow;
        event Action RightArrow;
        event Action DownArrow;
        event Action Spacebar;
    }
}
