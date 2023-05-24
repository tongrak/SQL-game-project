using System;
using UnityEngine;
public enum DatabaseChapter
{
    ChapterDemo,
    Chapter1
}

public class DatabaseFilePath
{
    public static string LocateDBPath(DatabaseChapter databaseFile)
    {
        string dbPath = "URI=file:" + Application.dataPath + "/Database/";
        switch (databaseFile)
        {
            case DatabaseChapter.ChapterDemo:
                dbPath += "DemoDatabase.db";
                break;
            case DatabaseChapter.Chapter1:
                dbPath += "Database1.db";
                break;
            default:
                throw new Exception("Database file is not real.");
        }
        return dbPath;
    }
}
