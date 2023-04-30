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

    public SQLResult CheckAnswer(string pQuery, string anQuery)
    {
        string ansResult;
        string pResult;
        SQLResult playerResult = new SQLResult();
        // get result from answer's query
        try
        {
            ansResult = GetQueryResult(anQuery);
        }
        catch (SqliteException e)
        {
            throw new ArgumentException("Answer's query:" + e.Message.ToString());
        }

        // get result from player's query
        try
        {
            pResult = GetQueryResult(pQuery);
            // Player query is correct
            if (ansResult.Equals(pResult))
            {
                playerResult.IsCorrect = true;
            }
            // Player query is not correct
            else
            {
                playerResult.IsCorrect = false;
            }
            playerResult.tableResult = pResult;
        }
        catch (SqliteException e)
        {
            playerResult.IsError = true;
            playerResult.tableResult = e.Message.ToString();
        }

        return playerResult;
    }

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
                    // open json form
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
                    for (int i = 0; i < reader.FieldCount-1; i++)
                    {
                        jsonResult[i] = jsonResult[i].Remove(jsonResult[i].Length - 1, 1);
                        jsonResult[i] += "},";
                        result += jsonResult[i];
                    }
                    // fill last last with '}'
                    jsonResult[reader.FieldCount-1] += "}";
                    result += jsonResult[reader.FieldCount-1];
                    // closed json form
                    result += "}";
                }
            }
            connection.Close();
        }
        return result;
    }

}
