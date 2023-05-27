using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PuzzleController
{
    public interface IQueryPuzzleController
    {
        [SerializeField] protected TextAsset puzzleFile { get; set; }
        [SerializeField] protected DatabaseChapter databaseChapter { get; set; }
    }
    public class QueryPuzzleControllerParent
    {
        [SerializeField] private TextAsset puzzleFile { get; set; }

        [SerializeField] private DatabaseChapter databaseChapter { get; set; }

        protected string DBPath { get; set; }
        protected string AnswerQuery { get; set; }
        protected bool[] CondStatus { get; set; }
        protected Condition Condition { get; set; }
        protected int currScore { get; set; }

        #region Interface methods

        public void ResetExecutedNum(ref int executedNum)
        {
            executedNum = 0;
        } 
        public PuzzleResult GetResult(string playerQuery, string dbPath, string answerQuery, Condition condition, ref int executedNum) 
        { 
            executedNum += 1;
            return PuzzleEvaluator.GetInstance().EvaluateQuery(dbPath, answerQuery, playerQuery, condition, executedNum);
        }
        #endregion

        #region For awake method
        // Load puzzle value from json file
        public void Load_QueryPuzzle(ref string[] puzzleDialog, ref string queryQuestion, ref string answerQuery, ref Condition condition, ref string[] conditionMessage, ref PuzzleResult currPuzzleResult, ref int executedNum, DatabaseChapter databaseChapter, ref string dbPath) 
        {
            QueryPuzzleModel puzzle = JsonUtility.FromJson<QueryPuzzleModel>(puzzleFile.text);

            puzzleDialog = puzzle.dialog;
            queryQuestion = puzzle.question;
            answerQuery = puzzle.answer;

            condition = puzzle.condition;
            conditionMessage = condition.GetConditionMessage();

            currPuzzleResult = new PuzzleResult(puzzle.condition);

            executedNum = 0;

            // locate used database path
            dbPath = DatabaseFilePath.LocateDBPath(databaseChapter);

            // validate answer query
            SQLValidator validator = SQLValidator.GetInstance();
            validator.validatePathAndQuery(dbPath, answerQuery);
        }
        #endregion

        #region For testing methods

        public string GetAnswerQuery()
        {
            return AnswerQuery;
        }
        #endregion
    }

    public class GetItemControllerParent
    {
        protected KeyItem keyItemCarried;

        public KeyItem GetItem()
        {
            return keyItemCarried;
        }

    }

    public interface ILockPuzzleController
    {
        [SerializeField] protected string[] UnityPreDialog { get; set; }
        [SerializeField] protected List<KeyItem> LockKeyItem { get; set; }
    }

    public class LockPuzzleControllerParent
    {
        [SerializeField] protected string[] UnityPreDialog { get; set; }
        [SerializeField] protected List<KeyItem> LockKeyItem { get; set; }

        public string[] PrePuzzleDialog { get; protected set; }

        public bool IsLock { get; protected set; }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            if (LockKeyItem.Contains(playerItem))
            {
                LockKeyItem.Remove(playerItem);
                if (LockKeyItem.Count == 0)
                {
                    UnlockPuzzle();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void UnlockPuzzle()
        {
            IsLock = false;
            Debug.Log("Puzzle is unlock");
        }

        protected void Load_LockPuzzle()
        {
            PrePuzzleDialog = UnityPreDialog;
        }
    }
}
