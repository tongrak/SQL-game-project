using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleController
{
    public interface IPuzzleController
    {
        #region Properties
        PuzzleType PuzzleType { get; }
        string[] PrePuzzleDialog { get; }
        string[] PuzzleDialog { get; }
        string QueryQuestion { get; }
        string[] ConditionMessage { get; }
        PuzzleResult CurrPuzzleResult { get; }
        bool IsLock { get; }
        #endregion

        #region Methods
        PuzzleResult GetResult(string playerQuery);
        void ResetExecutedNum();
        int GetExecutedNum();
        KeyItem GetKeyItem();
        bool InsertKeyItem(KeyItem playerItem);
        #endregion
    }
}