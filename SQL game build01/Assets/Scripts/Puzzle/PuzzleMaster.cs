using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using queryPuzzleModel;
using conditionModel;

public interface IPuzzleMaster
{
    public string DBPath { get;}
    public string[] Dialog { get; }
    public string Question { get; }
    public string AnswerQuery { get; }
    public Condition Condition { get; }

    public PuzzleResult GetResult(string playerQuery);

}

public class PuzzleMaster : MonoBehaviour, IPuzzleMaster
{

    [Header("Select puzzle type")]
    [SerializeField] PuzzleType puzzleType;

    [Header("Puzzle JSON file")]
    [SerializeField] TextAsset puzzleFile;

    [Header("Database")]
    [SerializeField] DatabaseFile databaseFile;

    public string DBPath { get; set; } = null;
    public string[] Dialog { get; set; } = null;
    public string Question { get; set; } = null;
    public string AnswerQuery { get; set; } = null;
    public Condition Condition { get; set; } = null;

    void Awake()
    {
        // 1) locate used database path
        DBPath = DatabaseFilePath.LocateDBPath(databaseFile);
        // 2) keep value from puzzle file to this object
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

    #region Result
    public PuzzleResult GetResult(string playerQuery)
    {
        PuzzleResult playerResult;
        try
        {
            // Check if playerQuery is invalid.
            SQLValidator.GetInstance().validatePathAndQuery(DBPath, playerQuery);
            playerResult = PuzzleEvaluator.GetInstance().EvalutateQuery(DBPath, AnswerQuery, playerQuery);
        }
        catch (SqliteException e)
        {
            playerResult = new PuzzleResult();
            playerResult.IsError = true;
            playerResult.ErrorMessage = e.Message.ToString();
        }
        return playerResult;
    }
    #endregion

    public void ConstructForTest(PuzzleType pt, string puzzleText, DatabaseFile df)
    {
        puzzleType = pt;
        TextAsset pf = new TextAsset(puzzleText);
        puzzleFile = pf;
        databaseFile = df;
    }

    public void buttonMethod()
    {
        Debug.Log("Question: " + Question);
        Debug.Log("Join: " + Condition.joinNum.ToString());
        Debug.Log("JoinNum is null: " + (Condition.joinNum.Equals("")).ToString());

    }

    // Load puzzle value from json file
    private void LoadPuzzle()
    {
        QueryPuzzleModel puzzle = JsonUtility.FromJson<QueryPuzzleModel>(puzzleFile.text);
        Dialog = puzzle.dialog;
        Question = puzzle.question;
        AnswerQuery = puzzle.answer;
        Condition = puzzle.condition;
    }
}
