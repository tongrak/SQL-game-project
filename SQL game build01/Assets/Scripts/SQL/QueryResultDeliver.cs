using Mono.Data.Sqlite;
using System;
using System.Data;
using System.Collections.Generic;

public class QueryResultDeliver
{
    private static QueryResultDeliver instance = new QueryResultDeliver();

    private QueryResultDeliver()
    {

    }

    public static QueryResultDeliver GetInstance()
    {
        return instance;
    }

    //// return result from query in json form.
    //public string GetQueryResult(string dbPath,string query)
    //{
    //    string result = "";
    //    // Connect to database
    //    using (SqliteConnection connection = new SqliteConnection(dbPath))
    //    {
    //        connection.Open();
    //        // Query to database
    //        using (SqliteCommand command = new SqliteCommand(query, connection))
    //        {
    //            // Read data from query
    //            using (IDataReader reader = command.ExecuteReader())
    //            {
    //                string[] jsonResult = new string[reader.FieldCount];
    //                // open json form
    //                result += "{";
    //                // set header in json
    //                for (int i = 0; i < reader.FieldCount; i++)
    //                {
    //                    jsonResult[i] += "\"" + reader.GetName(i) + "\": [";
    //                }
    //                // fill value for each header from each row in table
    //                while (reader.Read())
    //                {
    //                    for (int j = 0; j < reader.FieldCount; j++)
    //                    {
    //                        jsonResult[j] += "\"" +reader.GetValue(j).ToString()+ "\"";
    //                        jsonResult[j] += ",";
    //                    }
    //                }
    //                // Cut ',' from last element of each header except last header and fill last element of each header with ']' and ',' to close header
    //                for (int i = 0; i < reader.FieldCount - 1; i++)
    //                {
    //                    jsonResult[i] = jsonResult[i].Remove(jsonResult[i].Length - 1, 1);
    //                    jsonResult[i] += "],";
    //                    result += jsonResult[i];
    //                }
    //                // Cut ',' from last element of last header and fill last with ']'
    //                jsonResult[reader.FieldCount - 1] = jsonResult[reader.FieldCount - 1].Remove(jsonResult[reader.FieldCount - 1].Length - 1, 1);
    //                jsonResult[reader.FieldCount - 1] += "]";
    //                result += jsonResult[reader.FieldCount - 1];
    //                // closed json form
    //                result += "}";
    //            }
    //        }
    //        connection.Close();
    //    }
    //    return result;
    //}

    public string[][] GetQueryResult(string dbPath, string query)
    {
        string[][] queryResult;
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
                    queryResult = new string[reader.FieldCount][];
                    List<string> buffer = new List<string>();
                    // fill value for each header from each row in table
                    int attr_index = 0;
                    while (reader.Read())
                    {
                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                            buffer.Add(reader.GetValue(j).ToString());
                        }
                        queryResult[attr_index] = buffer.ToArray();
                        buffer.Clear();
                        attr_index++;
                    }
                }
            }
            connection.Close();
        }
        return queryResult;
    }
}
