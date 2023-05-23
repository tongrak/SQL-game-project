using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Data.Sqlite;

namespace PuzzleController
{
    public class PuzzleResult
    {
        public readonly string playerQuery;
        public readonly string[][] queryResult;
        public readonly List<bool> conditionResult;
        public readonly bool isError = false;
        public readonly string errorMessage;

        #region Constructors
        // Error PuzzleResult's constructor
        public PuzzleResult(Condition cond, string playerQuery, string errMessage)
        {
            this.playerQuery = playerQuery;
            errorMessage = errMessage;
            isError = true;

            int condNum = GetConditionNum(cond);
            conditionResult = new List<bool>();
            for (int i = 0; i < condNum; i++)
            {
                conditionResult.Add(false);
            }
        }

        // Normal PuzzleResult's constructor
        public PuzzleResult(string playerQuery, string[][] queryResult, List<bool> conditionResult)
        {
            this.playerQuery = playerQuery;
            this.queryResult = queryResult;
            this.conditionResult = conditionResult;
        }
        #endregion

        private int GetConditionNum(Condition cond)
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

        public PuzzleResult EvaluateQuery(string dbPath, string answerQuery, string playerQuery, Condition cond, int executedNum)
        {
            // Validate player's query
            try
            {
                // Check if playerQuery is invalid.
                SQLValidator.GetInstance().validatePathAndQuery(dbPath, playerQuery);
            }
            catch (SqliteException e)
            {
                //puzzleResult = new PuzzleResult();
                return new PuzzleResult(cond, playerQuery, e.Message.ToString());
            }

            string[][] queryResult = QueryResultDeliver.GetInstance().GetQueryResult(dbPath, playerQuery);
            List<bool> conditionResult = EvalByCond(cond, playerQuery, IsCorrectQuery(dbPath, answerQuery, playerQuery), executedNum);

            return new PuzzleResult(playerQuery, queryResult, conditionResult);
        }

        private List<bool> EvalByCond(Condition cond, string playerQuery, bool isQueryCorrect, int executedNum)
        {
            // Init list of condition's result
            List<bool> condResult = new List<bool>();
            condResult.Add(isQueryCorrect);

            string[] queryToken = CreateQueryToken(playerQuery);
            if (!cond.joinNum.Equals(null))
            {
                condResult.Add(JoinNumEval(cond.joinNum, queryToken));
            }
            if (!cond.haveJoin.Equals(null))
            {
                condResult.Add(HaveJoinEval(cond.haveJoin, queryToken));
            }
            if (!cond.nestedNum.Equals(null))
            {
                condResult.Add(NestedNumEval(cond.nestedNum, queryToken));
            }
            if (!cond.executeNum.Equals(null))
            {
                condResult.Add(ExecuteNumEval(cond.executeNum, executedNum));
            }
            //if (!cond.whereCondNum.Equals(null))
            //{
            //    WhereCondNumEval(cond.whereCondNum, playerQuery);
            //}

            return condResult;
        }

        #region Eval method for each condition
        private bool IsCorrectQuery(string dbPath, string answerQuery, string playerQuery)
        {
            QueryResultDeliver queryDeliver = QueryResultDeliver.GetInstance();
            // Get result from answer query
            string[][] answerResult = queryDeliver.GetQueryResult(dbPath, answerQuery);
            // Get result from player query
            string[][] playerResult = queryDeliver.GetQueryResult(dbPath, playerQuery);

            return IsEqualQueryResult(answerResult, playerResult);
        }

        private bool WhereCondNumEval(string whereCondNumCond, string[] queryToken)
        {
            // will implement later.
            return true;
        }

        private bool NestedNumEval(string nestedNumCond, string[] queryToken)
        {
            int maxNestedNum = int.Parse(nestedNumCond);
            string keyWordCond = "select";

            int playerNestedNum = -1;
            foreach (string token in queryToken)
            {
                if (token.Equals(keyWordCond))
                {
                    playerNestedNum += 1;
                }
            }

            return playerNestedNum <= maxNestedNum;
        }

        private bool ExecuteNumEval(string executeNumCond, int executedNum)
        {
            int maxExecuteNum = int.Parse(executeNumCond);
            return executedNum <= maxExecuteNum;
        }

        private bool JoinNumEval(string joinNumCond, string[] queryToken)
        {
            int maxJoinNum = int.Parse(joinNumCond);
            string keyWordCond = "join";

            int playerJoinNum = 0;
            foreach (string token in queryToken)
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
        #endregion

        #region Helper method
        private string[] CreateQueryToken(string playerQuery)
        {
            char[] delimiterChars = { ' ', ';', '\n' };
            string lowerPlayerQuery = playerQuery.ToLower();
            return lowerPlayerQuery.Split(delimiterChars);
        }

        private bool IsEqualQueryResult(string[][] query1, string[][] query2)
        {
            if (query1.Length != query2.Length)
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
        #endregion
    }
}

