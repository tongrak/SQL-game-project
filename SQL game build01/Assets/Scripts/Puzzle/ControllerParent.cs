using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PuzzleController
{
    public class QueryPuzzleControllerParent
    {
        [SerializeField] protected TextAsset puzzleFile;
        [SerializeField] protected DatabaseChapter databaseChapter;

        protected string DBPath { get; set; }
        protected string AnswerQuery { get; set; }
        public Condition Condition { get; protected set; }
        protected int currScore { get; set; }
        protected int ExecutedNum;

        #region Interface methods
        public void ResetExecutedNum()
        {
            ExecutedNum = 0;
        } 

        public PuzzleResult GetResult(string playerQuery) 
        { 
            ExecutedNum += 1;
            return PuzzleEvaluator.GetInstance().EvaluateQuery(DBPath, AnswerQuery, playerQuery, Condition, ExecutedNum);
        }

        public int GetExecutedNum()
        {
            return ExecutedNum;
        }
        #endregion

        #region For awake method
        // Load puzzle value from json file
        //public void Load_QueryPuzzle(ref string[] puzzleDialog, ref string queryQuestion, ref string[] conditionMessage, ref PuzzleResult currPuzzleResult) 
        //{
        //    QueryPuzzleModel puzzle = JsonUtility.FromJson<QueryPuzzleModel>(puzzleFile.text);

        //    puzzleDialog = puzzle.dialog;
        //    queryQuestion = puzzle.question;
        //    AnswerQuery = puzzle.answer;

        //    Condition = puzzle.condition;
        //    conditionMessage = Condition.GetConditionMessage();

        //    currPuzzleResult = new PuzzleResult(puzzle.condition);

        //    ResetExecutedNum();

        //    // locate used database path
        //    DBPath = DatabaseFilePath.LocateDBPath(databaseChapter);

        //    // validate answer query
        //    SQLValidator validator = SQLValidator.GetInstance();
        //    validator.validatePathAndQuery(DBPath, AnswerQuery);
        //}

        public void Load_QueryPuzzle(Action<string[]> setPuzzleDialog, Action<string> setQueryQuestion, Action<string[]> setConditionMessage, Action<PuzzleResult> setCurrPuzzleResult)
        {
            QueryPuzzleModel puzzle = JsonUtility.FromJson<QueryPuzzleModel>(puzzleFile.text);

            setPuzzleDialog(puzzle.dialog);
            setQueryQuestion(puzzle.question);
            AnswerQuery = puzzle.answer;

            Condition = puzzle.condition;
            setConditionMessage(Condition.GetConditionMessage());

            setCurrPuzzleResult(new PuzzleResult(puzzle.condition));

            ResetExecutedNum();

            // locate used database path
            DBPath = DatabaseFilePath.LocateDBPath(databaseChapter);

            // validate answer query
            SQLValidator validator = SQLValidator.GetInstance();
            validator.validatePathAndQuery(DBPath, AnswerQuery);
        }
        #endregion
    }

    public class GetItemControllerParent
    {
        [SerializeField] protected KeyItem keyItemCarried;

        #region Interface methods
        public KeyItem GetItem()
        {
            return keyItemCarried;
        }
        #endregion

    }

    public class LockPuzzleControllerParent
    {
        [SerializeField] protected string[] UnityPreDialog;
        [SerializeField] protected List<KeyItem> LockKeyItem;

        #region Interface methods
        //public bool InsertKeyItem(KeyItem playerItem, ref bool isLock)
        //{
        //    if (LockKeyItem.Contains(playerItem))
        //    {
        //        LockKeyItem.Remove(playerItem);
        //        if (LockKeyItem.Count == 0)
        //        {
        //            UnlockPuzzle(ref isLock);
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool InsertKeyItem(KeyItem playerItem, Action<bool> setIsLock)
        {
            if (LockKeyItem.Contains(playerItem))
            {
                LockKeyItem.Remove(playerItem);
                if (LockKeyItem.Count == 0)
                {
                    // Unlock the puzzle.
                    setIsLock(false);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region For awake method
        protected void Load_LockPuzzle(Action<string[]> setPrePuzzleDialog)
        {
            setPrePuzzleDialog(UnityPreDialog);
        }
        #endregion

        //protected void UnlockPuzzle(ref bool isLock)
        //{
        //    isLock = false;
        //    Debug.Log("Puzzle is unlock");
        //}

        
    }
}
