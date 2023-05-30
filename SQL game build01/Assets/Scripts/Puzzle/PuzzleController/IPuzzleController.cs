using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.PuzzleController
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

    public static class PuzzleControlExceptionMessage
    {
        #region Properties's exception message
        public static string noPrePuzzleDialog = "This puzzle doesn't have dialog for using key item.";
        public static string noIsLock = "This puzzle doesn't have to use key item to unlock.";
        public static string noConditionMessgage = "This puzzle doesn't have any condition";
        public static string noCurrPuzzleResult = "This puzzle doesn't have query result";
        #endregion

        #region Method's exception message
        public static string noGetResultMethod = "This puzzle doesn't have to query";
        public static string noGetKeyItemMethod = "This puzzle doesn't contain any key item";
        public static string noInsertKeyItemMethod = "This puzzle doesn't need key item to unlock";
        public static string noResetExecutedNumMethod = "This puzzle doesn't have to reset number of executed";
        public static string noGetExecutedNumMethod = "This puzzle doesn't have number of executed";
        #endregion
    }

    public interface IQueryPuzzle
    {
        int GetCurrScore();
    }
}