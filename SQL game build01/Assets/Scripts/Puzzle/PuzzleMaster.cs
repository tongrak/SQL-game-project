using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleMaster : MonoBehaviour
{
    enum PuzzleType { query, keyItem, queryAndKeyItem, fillQueryCommand, tellQueryResult }
    enum DatabaseFile
    {
        ChapterDemo,
        Chapter1
    }

    [Header("Select puzzle type")]
    [SerializeField] PuzzleType puzzleType;

    [Header("Puzzle text file")]
    [SerializeField] TextAsset textfile;

    [Header("Database")]
    [SerializeField] DatabaseFile databasefile;

    public string dbPath { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // 1) read text from puzzle file
        // 2) keep each value in this class
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonMethod()
    {
        PuzzleTypeCast();
        dbPath = LocateDBPath();
        Debug.Log(dbPath);
    }

    private void PuzzleTypeCast()
    {
        switch (puzzleType)
        {
            case PuzzleType.query:
                Debug.Log("This is query puzzle.");
                break;
            case PuzzleType.keyItem:
                Debug.Log("This is key item puzzle.");
                break;
            case PuzzleType.queryAndKeyItem:
                Debug.Log("This is query and key item puzzle.");
                break;
            case PuzzleType.fillQueryCommand:
                Debug.Log("This is fill query command puzzle.");
                break;
            default:
                Debug.Log("This is tell query result puzzle.");
                break;
        }
    }

    private string LocateDBPath()
    {
        string dbPath = "URI=file:" + Application.dataPath + "/Database/";
        switch (databasefile)
        {
            case DatabaseFile.ChapterDemo:
                Debug.Log("DemoDatabase.db");
                return dbPath += "DemoDatabase.db";
            case DatabaseFile.Chapter1:
                Debug.Log("Database1.db");
                return dbPath += "Database1.db";
            default:
                throw new Exception("Database file is not real.");
        }
    }
}
