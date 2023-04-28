using Mono.Data.Sqlite; // 1
using System;
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
        // get result from answer's query
        try
        {

        }
        catch (SqliteException e)
        {
            throw new ArgumentException("Answer's query:" + e.Message.ToString());
        }

        // get result from player's query
        try
        {

        }
        catch (SqliteException e)
        {

        }
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
        string result = "";
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
                    string[] jsonResult = new string[reader.FieldCount];
                    result += "{";
                    // set header in json
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        jsonResult[i] += "\""+reader.GetName(i)+"\": {";
                    }
                    // fill value for each header from each row in table
                    while (reader.Read())
                    {
                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                            jsonResult[j] += reader.GetValue(j).ToString();
                            jsonResult[j] += ",";
                        }
                    }
                    // fill last element of each header with '}' and ',' to close header
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        jsonResult[i] = jsonResult[i].Remove(jsonResult[i].Length - 1, 1);
                        if(i < reader.FieldCount - 1)
                        {
                            jsonResult[i] += "},";
                        }
                        else
                        {
                            jsonResult[i] += "}";
                        }
                        result += jsonResult[i];
                    }
                    result += "}";
                }
            }
            connection.Close();
        }
        return result;
    }

}
