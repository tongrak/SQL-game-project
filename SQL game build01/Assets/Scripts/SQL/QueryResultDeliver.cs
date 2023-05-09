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

    public string[][] GetQueryResult(string dbPath, string query)
    {
        string[][] queryResult;
        int numOfRecord = 0;
        // Connect to database
        using (SqliteConnection connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            // Query to database
            using (SqliteCommand command = new SqliteCommand(query, connection))
            {
                // Count number of record
                using(IDataReader forCountReader = command.ExecuteReader())
                {
                    while (forCountReader.Read())
                    {
                        numOfRecord += 1;
                    }
                }
                // Read data from query
                using (IDataReader reader = command.ExecuteReader())
                {
                    queryResult = new string[reader.FieldCount][];
                    List<string> buffer = new List<string>();
                    // set attribute in result
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        queryResult[i] = new string[numOfRecord + 1];
                        queryResult[i][0] = reader.GetName(i);
                    }
                    // fill value for each header from each row in table
                    int record_index = 1;
                    while (reader.Read())
                    {
                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                            queryResult[j][record_index] = reader.GetValue(j).ToString();
                        }
                        record_index++;
                    }
                }
            }
            connection.Close();
        }
        return queryResult;
    }
}
