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
        string AnswerQuery { get; }
        int ExecuteNum { get; }
        string[] ConditionMessage { get; }
        PuzzleResult CurrPuzzleResult { get; }
        bool isPassAllKeyItem { get; }
        #endregion

        #region Methods
        PuzzleResult GetResult();
        int GetKeyItem(string playerQuery);
        bool CheckKeyItem(int keyItem);
        #endregion
    }
}