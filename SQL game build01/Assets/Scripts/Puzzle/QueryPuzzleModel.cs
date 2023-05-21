using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using conditionModel;

namespace queryPuzzleModel
{
    [System.Serializable]
    public class QueryPuzzleModel
    {
        public string[] dialog;
        public string question;
        public string answer;
        public Condition condition;
    }
}

