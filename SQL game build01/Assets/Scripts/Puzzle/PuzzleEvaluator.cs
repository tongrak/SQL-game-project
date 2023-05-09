using System.Collections;
using System.Collections.Generic;

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

    public string EvalutateQuery(string dbPath ,string answerQuery, string playerQuery)
    {
        QueryResultDeliver queryDeliver = QueryResultDeliver.GetInstance();
        string result = "{queryResult:";
        // Get result from answer query
        string answerResult = queryDeliver.GetQueryResult(dbPath, answerQuery);
        // Get result from player query
        string playerResult = queryDeliver.GetQueryResult(dbPath, playerQuery);
        // Insert player query result
        result += playerResult + ",score:";
        // Check answer and player result if equal
        if (answerResult.Equals(playerResult))
        {
            result += "1";
        }
        else
        {
            result += "0";
        }

        result += "}";

        return result;
    }
}
