using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PuzzleController
{
    public class LockPuzzleController : MonoBehaviour, IPuzzleController
    {
        [SerializeField] protected string[] UnityDialog;
        [SerializeField] protected string UnityQuestion;

        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.LockPuzzle;

        public string[] Dialog { get; protected set; }

        public string Question { get; protected set; }

        public int ExecutedNum => throw new Exception("This puzzle doesn't have to execute query");

        public string[] ConditionMessage => throw new Exception("This puzzle doesn't have any condition");

        public PuzzleResult CurrPuzzleResult => throw new Exception("This puzzle doesn't have query result");

        public bool isUnlock { get; protected set; } = false;

        [SerializeField] protected List<KeyItem> LockedKeyItem;

        public PuzzleResult GetResult(string playerQuery)
        {
            throw new Exception("This puzzle doesn't have to query");
        }

        public KeyItem GetKeyItem()
        {
            throw new Exception("This puzzle doesn't have key item");
        }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            if (LockedKeyItem.Contains(playerItem))
            {
                LockedKeyItem.Remove(playerItem);
                if(LockedKeyItem.Count == 0)
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

        protected void LoadPuzzle()
        {
            Dialog = UnityDialog;
            Question = UnityQuestion;
        }

        void Awake()
        {
            LoadPuzzle();
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
