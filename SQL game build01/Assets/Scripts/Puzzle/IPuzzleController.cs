using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleController
{
    public interface IPuzzleController
    {
        #region Properties
        PuzzleType PuzzleType { get; }
        string[] Dialog { get; }
        string Question { get; }
        int ExecutedNum { get; }
        string[] ConditionMessage { get; }
        PuzzleResult CurrPuzzleResult { get; }
        bool isUnlock { get; }
        #endregion

        #region Methods
        PuzzleResult GetResult(string playerQuery);
        KeyItem GetKeyItem();
        bool InsertKeyItem(KeyItem playerItem);
        #endregion
    }
}