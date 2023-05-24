using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;

//public interface IPuzzleMaster
//{
//    string DBPath { get;}
//    string[] Dialog { get; }
//    string Question { get; }
//    string AnswerQuery { get; }
//    bool[] CondStatus { get; }
//    string[] ConditionMessage { get; }
//    PuzzleType puzzleType { get; }

//    public PuzzleResult GetResult(string playerQuery);
//    public int GetItemID();
//    public bool CheckItemID(int itemID);
//}

namespace PuzzleController
{
    public class QueryPuzzleController : MonoBehaviour, IPuzzleController
    {

        [Header("Puzzle JSON file")]
        [SerializeField] protected TextAsset puzzleFile;

        [Header("Database")]
        [SerializeField] protected DatabaseChapter databaseChapter;

        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.QueryPuzzle;
        public string[] Dialog { get; protected set; } = null;
        public string Question { get; protected set; } = null;
        public int ExecutedNum { get; protected set; } = 0;
        public string[] ConditionMessage { get; protected set; }
        public PuzzleResult CurrPuzzleResult { get; protected set; }
        public bool isUnlock { get; protected set; } = true;

        protected string DBPath { get; set; } = null;
        protected string AnswerQuery { get; set; } = null;
        protected bool[] CondStatus { get; set; } = null;
        protected Condition Condition { get; set; } = null;

        void Awake()
        {
            // keep value from puzzle file to this object
            LoadPuzzle();
            // 3) validate answer query
            SQLValidator validator = SQLValidator.GetInstance();
            validator.validatePathAndQuery(DBPath, AnswerQuery);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ResetExecutedNum()
        {
            ExecutedNum = 0;
        }

        #region Interface methods
        public PuzzleResult GetResult(string playerQuery)
        {
            ExecutedNum += 1;
            return PuzzleEvaluator.GetInstance().EvaluateQuery(DBPath, AnswerQuery, playerQuery, Condition, ExecutedNum);
        }

        public KeyItem GetKeyItem()
        {
            throw new Exception("This puzzle doesn't contain any key item");
        }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            throw new Exception("This puzzle doesn't need key item to unlock");
        }
        #endregion

        #region Private methods
        // Load puzzle value from json file
        protected void LoadPuzzle()
        {
            QueryPuzzleModel puzzle = JsonUtility.FromJson<QueryPuzzleModel>(puzzleFile.text);
            Dialog = puzzle.dialog;
            Question = puzzle.question;
            AnswerQuery = puzzle.answer;

            Condition = puzzle.condition;
            ConditionMessage = Condition.GetConditionMessage();

            CurrPuzzleResult = new PuzzleResult(puzzle.condition);

            ExecutedNum = 0;

            // locate used database path
            DBPath = DatabaseFilePath.LocateDBPath(databaseChapter);
        }
        #endregion

        #region For testing methods
        public void ConstructForTest(PuzzleType pt, string puzzleText, DatabaseChapter df)
        {
            PuzzleType = pt;
            TextAsset pf = new TextAsset(puzzleText);
            puzzleFile = pf;
            databaseChapter = df;
        }

        public void buttonMethod()
        {
            Debug.Log("Question: " + Question);
            Debug.Log("Join: " + Condition.joinNum.ToString());
            Debug.Log("JoinNum is null: " + (Condition.joinNum.Equals("")).ToString());

        }

        public string GetAnswerQuery()
        {
            return AnswerQuery;
        }
        #endregion

    }
}
