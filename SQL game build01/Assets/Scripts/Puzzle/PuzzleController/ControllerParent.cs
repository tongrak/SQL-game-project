using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Puzzle.PuzzleController
{
    [Serializable]
    public class QueryPuzzleControllerParent
    {
        [SerializeField] protected TextAsset puzzleFile;
        [SerializeField] protected DatabaseChapter databaseChapter;
        [SerializeField] protected QueryPuzzleScoreManager queryPScoreManager;

        protected string DBPath { get; set; }
        protected string AnswerQuery { get; set; }
        public Condition Condition { get; protected set; }
        protected int currScore { get; set; } = 0;
        protected int ExecutedNum;

        #region Interface methods
        public void ResetExecutedNum()
        {
            ExecutedNum = 0;
        }

        public PuzzleResult GetResult(string playerQuery, Action<PuzzleResult> SetCurrPuzzleResult)
        {
            ExecutedNum += 1;
            PuzzleResult latestPuzzleResult = PuzzleEvaluator.GetInstance().EvaluateQuery(DBPath, AnswerQuery, playerQuery, Condition, ExecutedNum);
            UpdateCurrPResultAndScore(latestPuzzleResult, SetCurrPuzzleResult);
            return latestPuzzleResult;
        }

        public int GetExecutedNum()
        {
            return ExecutedNum;
        }

        public int GetCurrScore()
        {
            return currScore;
        }
        #endregion

        #region For awake method
        // Load puzzle value from json file
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

        private void UpdateCurrPResultAndScore(PuzzleResult latestPuzzleResult, Action<PuzzleResult> SetCurrPuzzleResult)
        {
            int latestScore = PuzzleEvaluator.GetInstance().CalculateQueryScore(latestPuzzleResult.conditionResult);
            if(latestScore > currScore)
            {
                // Update current PuzzleResult
                SetCurrPuzzleResult(latestPuzzleResult);

                // Update puzzle score and total score in manager
                queryPScoreManager.AddScore(latestScore - currScore);
                currScore = latestScore;
            }
        }
    }

    [Serializable]
    public class GetItemPuzzleControllerParent
    {
        [SerializeField] protected KeyItem keyItemCarried;

        #region Interface methods
        public KeyItem GetKeyItem()
        {
            return keyItemCarried;
        }
        #endregion

    }
    
    [Serializable]
    public class LockPuzzleControllerParent
    {
        [SerializeField] protected string[] UnityPreDialog;
        [SerializeField] protected List<KeyItem> LockKeyItem;

        protected List<KeyItem> leftLockKeyItem;

        #region Interface methods
        public bool InsertKeyItem(KeyItem playerItem, Action<bool> setIsLock)
        {
            if (leftLockKeyItem.Contains(playerItem))
            {
                leftLockKeyItem.Remove(playerItem);
                if (leftLockKeyItem.Count == 0)
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
        public void Load_LockPuzzle(Action<string[]> setPrePuzzleDialog)
        {
            setPrePuzzleDialog(UnityPreDialog);
            leftLockKeyItem = LockKeyItem;
        }
        #endregion
    }
}
