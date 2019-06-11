using System;
using System.Timers;

namespace Tetris.Model
{
    interface IGameEngine
    {
        Player Player { get; set; }
        TetrisBoard TetrisBoard { get; set; }
        Tetromino[] Tetrominos { get; set; }
        Timer Timer { get; set; }

        event Action AddScore;
        event Action Changes;

        void MoveLeft();
        void MoveRight();
        void FallDown();
        void Run();
        void Stop();
        void Timer_Elapsed();
    }
}