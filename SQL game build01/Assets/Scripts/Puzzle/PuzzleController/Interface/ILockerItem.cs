using Puzzle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.PuzzleController.Interface
{
    public interface ILockerItem
    {
        bool IsLocked { get; }
        string[] Dialog { get; }

        bool InsertKeyItem(KeyItem playerItem);
    }
}
