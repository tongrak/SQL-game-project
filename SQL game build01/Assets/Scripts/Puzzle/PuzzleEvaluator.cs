using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using conditionModel;
using Mono.Data.Sqlite;

public class PuzzleResult
{
    public readonly string[][] queryResult;
    //public int Score { get; set; } = 0;
    public readonly bool isError = false;
    public readonly string errorMessage;

    public readonly bool[] conditionResult;
    public readonly string playerQuery;

    public PuzzleResult(string errMessage)
    {
        errorMessage = errMessage;
        isError = true;
    }

    public PuzzleResult(string playerQuery, string[][] queryResult, bool[] conditionResult)
    {
        this.playerQuery = playerQuery;
        this.queryResult = queryResult;
        this.conditionResult = conditionResult;
    }
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

    public PuzzleResult EvaluateQuery(string dbPath, string answerQuery, string playerQuery)
    {
        //QueryResultDeliver queryDeliver = QueryResultDeliver.GetInstance();
        //PuzzleResult result = new PuzzleResult();
        //// Get result from answer query
        //string[][] answerResult = queryDeliver.GetQueryResult(dbPath, answerQuery);
        //// Get result from player query
        //string[][] playerResult = queryDeliver.GetQueryResult(dbPath, playerQuery);

        //result.queryResult = playerResult;
        //if(IsEqualQueryResult(answerResult, playerResult))
        //{
        //    result.Score = 1;
        //}
        //else
        //{
        //    result.Score = 0;
        //}
        //return result;
        return null;
    }

    public PuzzleResult EvaluateQuery(string dbPath, string answerQuery, string playerQuery, Condition cond)
    {
        //PuzzleResult puzzleResult = new PuzzleResult();
        // Validate player's query
        try
        {
            // Check if playerQuery is invalid.
            SQLValidator.GetInstance().validatePathAndQuery(dbPath, playerQuery);
        }
        catch (SqliteException e)
        {
            //puzzleResult = new PuzzleResult();
            return new PuzzleResult(e.Message.ToString());
        }

        string[][] queryResult = QueryResultDeliver.GetInstance().GetQueryResult(dbPath, playerQuery);
        List<bool> conditionResult;

        //puzzleResult.Score += CorrectNessEval(dbPath ,answerQuery, playerQuery);
        CreateQueryToken(playerQuery);
        EvalByCond(cond, playerQuery, IsCorrectQuery(dbPath, answerQuery, playerQuery));
        return null;
    }

    private int ConditionNum(Condition cond)
    {
        int condNum = 1;  // Init number of condition with 1 because correctness query must have in every puzzle.  
        if (!cond.joinNum.Equals(null))
        {
            condNum += 1;
        }
        if (!cond.haveJoin.Equals(null))
        {
            condNum += 1;
        }
        if (!cond.nestedNum.Equals(null))
        {
            condNum += 1;
        }
        if (!cond.executeNum.Equals(null))
        {
            condNum += 1;
        }
        if (!cond.whereCondNum.Equals(null))
        {
            condNum += 1;
        }

        return condNum;
    }

    private List<bool> EvalByCond(Condition cond, string playerQuery, bool isQueryCorrect)
    {
        string[] queryToken = CreateQueryToken(playerQuery);
        if (!cond.joinNum.Equals(null))
        {
            JoinNumEval(cond.joinNum, queryToken);
        }
        if (!cond.haveJoin.Equals(null))
        {
            HaveJoinEval(cond.haveJoin, queryToken);
        }
        if (!cond.nestedNum.Equals(null))
        {
            NestedNumEval(cond.nestedNum, playerQuery);
        }
        if (!cond.executeNum.Equals(null))
        {
            ExecuteNumEval(cond.executeNum, playerQuery);
        }
        if (!cond.whereCondNum.Equals(null))
        {
            WhereCondNumEval(cond.whereCondNum, playerQuery);
        }

        return null;
    }

    private string[] CreateQueryToken(string playerQuery)
    {
        char[] delimiterChars = { ' ', ';', '\n'};
        string lowerPlayerQuery = playerQuery.ToLower();
        return lowerPlayerQuery.Split(delimiterChars);
    }

    private bool IsCorrectQuery(string dbPath, string answerQuery, string playerQuery)
    {
        QueryResultDeliver queryDeliver = QueryResultDeliver.GetInstance();
        // Get result from answer query
        string[][] answerResult = queryDeliver.GetQueryResult(dbPath, answerQuery);
        // Get result from player query
        string[][] playerResult = queryDeliver.GetQueryResult(dbPath, playerQuery);

        if (IsEqualQueryResult(answerResult, playerResult))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void WhereCondNumEval(string whereCondNum, string playerQuery)
    {
        throw new System.NotImplementedException();
    }

    private void NestedNumEval(string nestedNum, string playerQuery)
    {
        throw new System.NotImplementedException();
    }

    private void ExecuteNumEval(string executeNum, string playerQuery)
    {
        throw new System.NotImplementedException();
    }

    private bool JoinNumEval(string joinNumCond, string[] queryToken)
    {
        int maxJoinNum = int.Parse(joinNumCond);
        string keyWordCond = "join";

        int playerJoinNum = 0;
        foreach(string token in queryToken)
        {
            if (token.Equals(keyWordCond))
            {
                playerJoinNum += 1;
            }
        }

        return playerJoinNum <= maxJoinNum;
    }

    private bool HaveJoinEval(string haveJoinCond, string[] queryToken)
    {
        bool haveJoin = Convert.ToBoolean(haveJoinCond);
        string keyWordCond = "join";

        int pos = Array.IndexOf(queryToken, keyWordCond);
        
        // the array contains the string and the pos variable
        if (pos > -1)
        {
            return haveJoin;
        }
        else
        {
            return !haveJoin;
        }
    }

    private bool IsEqualQueryResult(string[][] query1, string[][] query2)
    {
        if(query1.Length != query2.Length)
        {
            return false;
        }
        // Check each attribute
        else
        {
            for (int i = 0; i < query1.Length; i++)
            {
                // Check number of record from each attribute
                if (!query1[i].SequenceEqual(query2[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
