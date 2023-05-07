using System;
using UnityEngine;
public enum DatabaseFile
{
    ChapterDemo,
    Chapter1
}

public class DatabaseFilePath
{
    public static string LocateDBPath(DatabaseFile databaseFile)
    {
        string dbPath = "URI=file:" + Application.dataPath + "/Database/";
        switch (databaseFile)
        {
            case DatabaseFile.ChapterDemo:
                dbPath += "DemoDatabase.db";
                break;
            case DatabaseFile.Chapter1:
                dbPath += "Database1.db";
                break;
            default:
                throw new Exception("Database file is not real.");
        }
        return dbPath;
    }
}
