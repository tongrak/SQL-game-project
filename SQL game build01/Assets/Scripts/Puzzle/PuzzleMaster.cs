using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;

interface PuzzleMasterInt
{
    public string DBPath { get;}
    public string[] Dialog { get; }
    public string Question { get; }
    public string AnswerQuery { get; }
    public string[] Condition { get; }

    public string GetResult(string playerQuery)
    {
        return null;
    }

}

public class PuzzleMaster : MonoBehaviour, PuzzleMasterInt
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

    public string GetResult(string playerQuery)
    {
        string result;
        try
        {
            // Check if playerQuery is invalid.
            SQLValidator.GetInstance().validatePathAndQuery(DBPath, playerQuery);
            result = PuzzleEvaluator.GetInstance().EvalutateQuery(DBPath, AnswerQuery, playerQuery);
        }
        catch (SqliteException e) {
            result = "{error:\"" + e.Message.ToString() + "\",score:0}";
        }

        return result;
    }

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
