using Assets.Scripts.Puzzle.PuzzleController.Interface;
using Puzzle;
using System;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    public class QueryPuzzle : MonoBehaviour, IQueryPuzzle
    {
        [SerializeField] protected TextAsset puzzleFile;
        [SerializeField] protected DatabaseChapter databaseChapter;

        private ScoreManager scoreManager;

        private string DBPath { get; set; }
        private string AnswerQuery { get; set; }
        private Condition Condition { get; set; }

        public string[] Dialog { get; private set; }
        public string Question { get; private set; }
        public string[] ConditionMessage { get; private set; }
        public int CurrScore { get; private set; } = 0;
        public int ExecutedNum { get; private set; } = 0;
        public PuzzleResult BestPuzzleResult { get; private set; }

        public event EventHandler OnQueryCorrect;

        public PuzzleResult AnswerPuzzle(string playerQuery)
        {
            ExecutedNum += 1;
            PuzzleResult latestPuzzleResult = PuzzleEvaluator.GetInstance().EvaluateQuery(DBPath, AnswerQuery, playerQuery, Condition, ExecutedNum);

            // Invoke event when query correct
            if (latestPuzzleResult.conditionResult[0] == true)
            {
                OnQueryCorrect?.Invoke(this, EventArgs.Empty);
            }

            UpdateCurrPResultAndScore(latestPuzzleResult);
            return latestPuzzleResult;
        }

        public void ResetExecutedNum()
        {
            ExecutedNum = 0;
        }

        private void UpdateCurrPResultAndScore(PuzzleResult latestPuzzleResult)
        {
            int latestScore = PuzzleEvaluator.GetInstance().CalculateQueryScore(latestPuzzleResult.conditionResult);
            if (latestScore > CurrScore)
            {
                // Update current PuzzleResult
                BestPuzzleResult = latestPuzzleResult;

                // Update puzzle score and total score in manager
                scoreManager.AddScore(latestScore - CurrScore);
                CurrScore = latestScore;
            }
        }

        #region For awake method
        // Load puzzle value from json file
        private void Load_QueryPuzzle()
        {
            QueryPuzzleModel puzzle = JsonUtility.FromJson<QueryPuzzleModel>(puzzleFile.text);

            Dialog = puzzle.dialog;
            Question = puzzle.question;
            AnswerQuery = puzzle.answer;

            Condition = puzzle.condition;
            ConditionMessage = Condition.GetConditionMessage();

            BestPuzzleResult = new PuzzleResult(puzzle.condition);

            ResetExecutedNum();

            // locate used database path
            DBPath = DatabaseFilePath.LocateDBPath(databaseChapter);

            // validate answer query
            SQLValidator validator = SQLValidator.GetInstance();
            validator.validatePathAndQuery(DBPath, AnswerQuery);
        }
        #endregion

        void Awake()
        {
            Load_QueryPuzzle();
        }

        // Use this for initialization
        void Start()
        {
            scoreManager = GetComponent<PuzzleController>().ScoreManager;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}