using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Linq;
using System;
using System.Data;

public class SQLValidator
{
    private string[] bannedWord = { "create", "update", "delete", "insert", "drop", "alter", "truncate", "grant", "revoke", "commit", "rollback", "savepoint" };

    private static SQLValidator instance = new SQLValidator();

    private SQLValidator()
    {

    }

    public static SQLValidator GetInstance()
    {
        return instance;
    }

    //public bool isQueryInvalid(string dbPath,string query)
    //{
    //    if (haveBannedWord(query))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        try
    //        {
    //            QueryResultDeliver.GetInstance().GetQueryResult(dbPath, query);
    //        }
    //        catch (SqliteException e)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //}

    // Method for check if query is valid
    // if not valid, it will throw error
    // else nothing happen.
    public void validatePathAndQuery(string dbPath,string query)
    {
        if (haveBannedWord(query))
        {
            throw new SqliteException(WarningWord_BannedWord());
        }
        else
        {
            // Connect to database
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();
                // Query to database
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    // Read data from query
                    using (IDataReader reader = command.ExecuteReader())
                    {

                    }
                }
                connection.Close();
            }
        }
    }

    public bool haveBannedWord(string query)
    {
        string[] sqlWords = query.ToLower().Split(' ', ';');

        foreach (string word in sqlWords)
        {
            if (bannedWord.Contains(word))
            {
                return true;
            }
        }

        return false;
    }

    // Warning word for query that use banned word.
    private string WarningWord_BannedWord()
    {
        string warningWord = "You don't have permission to use this command:";

        for (int i = 0; i < bannedWord.Length; i++)
        {
            warningWord += " \"" + bannedWord[i] + "\"";
            if (i < bannedWord.Length - 1)
            {
                warningWord += ",";
            }
        }

        return warningWord;
    }
}
