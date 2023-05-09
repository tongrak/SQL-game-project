using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerResult
{
    public string[][] QueryResult { get; set; }
    public int Score { get; set; }
    public bool IsError { get; set; } = false;
    public string ErrorMessage { get; set; }

    //public Result(string[][] qr, int score)
    //{
    //    QueryResult = qr;
    //    Score = score;
    //}
}

public class PuzzleEvaluator
{
    private static PuzzleEvaluator instance = new PuzzleEvaluator();
    

    private PuzzleEvaluator()
    {

    }

    public static PuzzleEvaluator GetInstance()
    {
        return instance;
    }

    //public string EvalutateQuery(string dbPath ,string answerQuery, string playerQuery)
    //{
    //    QueryResultDeliver queryDeliver = QueryResultDeliver.GetInstance();
    //    string result = "{queryResult:";
    //     Get result from answer query
    //    string answerResult = queryDeliver.GetQueryResult(dbPath, answerQuery);
    //     Get result from player query
    //    string playerResult = queryDeliver.GetQueryResult(dbPath, playerQuery);
    //     Insert player query result
    //    result += playerResult + ",score:";
    //     Check answer and player result if equal
    //    if (answerResult.Equals(playerResult))
    //    {
    //        result += "1";
    //    }
    //    else
    //    {
    //        result += "0";
    //    }

    //    result += "}";

    //    return result;
    //}

    public PlayerResult EvalutateQuery(string dbPath, string answerQuery, string playerQuery)
    {
        QueryResultDeliver queryDeliver = QueryResultDeliver.GetInstance();
        PlayerResult result = new PlayerResult();
        // Get result from answer query
        string[][] answerResult = queryDeliver.GetQueryResult(dbPath, answerQuery);
        // Get result from player query
        string[][] playerResult = queryDeliver.GetQueryResult(dbPath, playerQuery);

        result.QueryResult = playerResult;
        if(isCorrectQuery(answerResult, playerResult))
        {
            result.Score = 1;
        }
        else
        {
            result.Score = 0;
        }
        return result;
    }

    private bool isCorrectQuery(string[][] arr1, string[][] arr2)
    {
        if(arr1.Length != arr2.Length)
        {
            return false;
        }
        // Check each attribute
        else
        {
            for (int i = 0; i < arr1.Length; i++)
            {
                // Check number of record from each attribute
                if (arr1[i].SequenceEqual(arr2[i]))
                {
                    return false;
                }
                else
                {
                    return true;
                }
                //for (int j = 0; j < arr1[i].Length; j++)
                //{
                //    if (!(arr1[i][j].Equals(arr2[i][j])))
                //    {
                //        return false;
                //    }
                //    else
                //    {
                //        return true;
                //    }
                //}
            }
        }

    }
}
