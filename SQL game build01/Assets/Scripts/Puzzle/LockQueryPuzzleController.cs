using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PuzzleController
{
    public class LockQueryPuzzleController : QueryPuzzleController, IPuzzleController
    {
        public new PuzzleType PuzzleType { get; protected set; } = PuzzleType.LockQueryPuzzle;
        public new bool isUnlock { get; protected set; } = false;

        [SerializeField] protected List<KeyItem> LockedKeyItem;

        public new PuzzleResult GetResult(string playerQuery)
        {
            if (!isUnlock)
            {
                string errMessage = "Player must use key item to unlock this puzzle.";
                return new PuzzleResult(Condition, "", errMessage);
            }
            else
            {
                ExecutedNum += 1;
                return PuzzleEvaluator.GetInstance().EvaluateQuery(DBPath, AnswerQuery, playerQuery, Condition, ExecutedNum);
            }
        }

        public new bool InsertKeyItem(KeyItem playerItem)
        {
            if (LockedKeyItem.Contains(playerItem))
            {
                LockedKeyItem.Remove(playerItem);
                if (LockedKeyItem.Count == 0)
                {
                    UnlockPuzzle();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void UnlockPuzzle()
        {
            isUnlock = true;
            Debug.Log("Puzzle is unlock");
        }

        void Awake()
        {
            // keep value from puzzle file to this object
            LoadPuzzle();
            // 3) validate answer query
            SQLValidator validator = SQLValidator.GetInstance();
            validator.validatePathAndQuery(DBPath, AnswerQuery);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}