using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PuzzleResult
{
    public string[][] QueryResult { get; set; }
    public int Score { get; set; }
    public bool IsError { get; set; } = false;
    public string ErrorMessage { get; set; }
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

    public PuzzleResult EvalutateQuery(string dbPath, string answerQuery, string playerQuery)
    {
        QueryResultDeliver queryDeliver = QueryResultDeliver.GetInstance();
        PuzzleResult result = new PuzzleResult();
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
                if (!arr1[i].SequenceEqual(arr2[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
