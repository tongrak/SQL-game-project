using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite; // 1
using System.Data; // 1
using UnityEngine;

public class SQLChecker
{
    private string dbPath;

    public SQLChecker(string dbPath)
    {
        this.dbPath = dbPath;
    }

    public bool CheckAnswer(string pQuery, string anQuery)
    {
        //return GetQueryResult(pQuery).Equals(GetQueryResult(anQuery));
        return true;
    }

    //public string GetPlayerResult(string pQuery)
    //{
    //    return "";
    //}

    //public string GetAnswerResult(string anQuery)
    //{
    //    return "";
    //}

    // return result from query in json form.
    public string GetQueryResult(string query)
    {
        //string result;
        try
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
                        while (reader.Read())
                        {
                            Debug.Log(reader.GetValue(1).ToString());
                        }
                    }
                }
                connection.Close();
            }
        }
        catch (SqliteException e)
        {
            Debug.Log(e.Message);
            return e.Message;
        }
        return "";
    }

}
