using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleController
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

