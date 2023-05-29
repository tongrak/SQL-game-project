using System;
using System.Collections;
using System.Collections.Generic;

namespace PuzzleController
{
    [System.Serializable]
    public class Condition
    {
        public string joinNum;
        public string haveJoin;
        public string nestedNum;
        public string executeNum;
        public string whereCondNum;

        public string[] GetConditionMessage()
        {
            List<string> condMessage = new List<string>();

            condMessage.Add("Correct the query");
            if (!joinNum.Equals(null))
            {
                condMessage.Add(GetjoinNumMessage());
            }
            if (!haveJoin.Equals(""))
            {
                condMessage.Add(GetHaveJoinMessage());
            }
            if (!nestedNum.Equals(null))
            {
                condMessage.Add(GetNestedNumMessage());
            }
            if (!executeNum.Equals(null))
            {
                condMessage.Add(GetExecuteNumMessage());
            }
            if (!whereCondNum.Equals(null))
            {
                condMessage.Add(GetWhereCondNumMessage());
            }

            return condMessage.ToArray();
        }

        public int GetConditionNum()
        {
            int condNum = 1;  // Init number of condition with 1 because correctness query must have in every puzzle.  
            if (!joinNum.Equals(null))
            {
                condNum += 1;
            }
            if (!haveJoin.Equals(null))
            {
                condNum += 1;
            }
            if (!nestedNum.Equals(null))
            {
                condNum += 1;
            }
            if (!executeNum.Equals(null))
            {
                condNum += 1;
            }
            if (!whereCondNum.Equals(null))
            {
                condNum += 1;
            }

            return condNum;
        }

        private string GetjoinNumMessage()
        {
            return "Use JOIN command less than " + joinNum + " time";
        }

        private string GetHaveJoinMessage()
        {
            if (Convert.ToBoolean(haveJoin))
            {
                return "Use JOIN command";
            }
            else
            {
                return "Can't use JOIN command";
            }
        }

        private string GetNestedNumMessage()
        {
            return "Use nested query less than " + nestedNum + " time";
        }

        private string GetExecuteNumMessage()
        {
            return "Execute query less than " + executeNum + " time";
        }

        private string GetWhereCondNumMessage()
        {
            return "Use condition in WHERE command less than " + whereCondNum + " time";
        }
    }
}
