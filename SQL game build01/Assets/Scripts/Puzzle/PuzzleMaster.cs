using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;

public interface IPuzzleMaster
{
    public string DBPath { get;}
    public string[] Dialog { get; }
    public string Question { get; }
    public string AnswerQuery { get; }
    public string[] Condition { get; }

    string GetResult(string playerQuery);

    string[][] GetResultV2(string playerQuery);

}

public class PuzzleMaster : MonoBehaviour, IPuzzleMaster
{

    [Header("Select puzzle type")]
    [SerializeField] PuzzleType puzzleType;

    [Header("Puzzle JSON file")]
    [SerializeField] TextAsset puzzleFile;

    [Header("Database")]
    [SerializeField] DatabaseFile databaseFile;

    public string DBPath { get; set; }
    public string[] Dialog { get; set; }
    public string Question { get; set; }
    public string AnswerQuery { get; set; }
    public string[] Condition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // 1) locate used database path
        DBPath = DatabaseFilePath.LocateDBPath(databaseFile);
        // 2) keep value from puzzle file to this object
        LoadPuzzle();
        // 3) validate answer query
        SQLValidator validator = SQLValidator.GetInstance();
        validator.validatePathAndQuery(DBPath, AnswerQuery);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Result
    public PlayerResult GetResult(string playerQuery)
    {
        PlayerResult playerResult;
        try
        {
            // Check if playerQuery is invalid.
            SQLValidator.GetInstance().validatePathAndQuery(DBPath, playerQuery);
            playerResult = PuzzleEvaluator.GetInstance().EvalutateQuery(DBPath, AnswerQuery, playerQuery);
        }
        catch (SqliteException e)
        {
            playerResult = new PlayerResult();
            playerResult.IsError = true;
            playerResult.ErrorMessage = e.Message.ToString();
        }
        return playerResult;
    }
    public string[][] GetResultV2(string playerQuery)
    {
        throw new NotImplementedException();
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
        //PuzzleTypeCast();
        //DBPath = LocateDBPath();
        //Debug.Log(DBPath);
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
