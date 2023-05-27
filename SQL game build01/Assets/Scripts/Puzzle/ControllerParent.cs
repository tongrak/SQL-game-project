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
        protected Condition Condition { get; set; }
        protected int currScore { get; set; }

        #region Interface methods
        public void ResetExecutedNum(ref int executedNum)
        {
            executedNum = 0;
        } 

        public PuzzleResult GetResult(string playerQuery, ref int executedNum) 
        { 
            executedNum += 1;
            return PuzzleEvaluator.GetInstance().EvaluateQuery(DBPath, AnswerQuery, playerQuery, Condition, executedNum);
        }
        #endregion

        #region For awake method
        // Load puzzle value from json file
        public void Load_QueryPuzzle(ref string[] puzzleDialog, ref string queryQuestion, ref string[] conditionMessage, ref PuzzleResult currPuzzleResult, ref int executedNum) 
        {
            QueryPuzzleModel puzzle = JsonUtility.FromJson<QueryPuzzleModel>(puzzleFile.text);

            puzzleDialog = puzzle.dialog;
            queryQuestion = puzzle.question;
            AnswerQuery = puzzle.answer;

            Condition = puzzle.condition;
            conditionMessage = Condition.GetConditionMessage();

            currPuzzleResult = new PuzzleResult(puzzle.condition);

            ResetExecutedNum(ref executedNum);

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

        public KeyItem GetItem()
        {
            return keyItemCarried;
        }

    }

    public class LockPuzzleControllerParent
    {
        [SerializeField] protected string[] UnityPreDialog;
        [SerializeField] protected List<KeyItem> LockKeyItem;

        public bool InsertKeyItem(KeyItem playerItem, ref bool isLock)
        {
            if (LockKeyItem.Contains(playerItem))
            {
                LockKeyItem.Remove(playerItem);
                if (LockKeyItem.Count == 0)
                {
                    UnlockPuzzle(ref isLock);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void UnlockPuzzle(ref bool isLock)
        {
            isLock = false;
            Debug.Log("Puzzle is unlock");
        }

        protected void Load_LockPuzzle(ref string[] PrePuzzleDialog)
        {
            PrePuzzleDialog = UnityPreDialog;
        }
    }
}
