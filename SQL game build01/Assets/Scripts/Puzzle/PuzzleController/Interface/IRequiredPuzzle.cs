﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Puzzle.PuzzleController.Interface
{
    public interface IRequiredPuzzle
    {
        bool IsLock { get; }

        void UnLock();
    }
}
