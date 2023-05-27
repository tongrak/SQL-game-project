using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PuzzleController
{
    public class LockPuzzleController : MonoBehaviour, IPuzzleController
    {
        #region Interface's properties
        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.LockPuzzle;
        public string[] PrePuzzleDialog { get; protected set; }
        public string[] PuzzleDialog { get; protected set; }
        public string QueryQuestion { get; protected set; }
        public int ExecutedNum => throw new Exception("This puzzle doesn't have to execute query");
        public string[] ConditionMessage => throw new Exception("This puzzle doesn't have any condition");
        public PuzzleResult CurrPuzzleResult => throw new Exception("This puzzle doesn't have query result");
        public bool IsLock { get; protected set; } = false;
        #endregion

        [SerializeField] protected LockPuzzleControllerParent lockPControl = new LockPuzzleControllerParent();

        #region Interface's methods
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
            return lockPControl.InsertKeyItem(playerItem, value => IsLock = value);
        }

        public void ResetExecutedNum()
        {
            throw new Exception("This puzzle doesn't have to reset number of executed");
        }

        public int GetExecutedNum()
        {
            throw new Exception("This puzzle doesn't have number of executed");
        }
        #endregion

        #region Unity's method
        void Awake()
        {
            lockPControl.Load_LockPuzzle(value => PrePuzzleDialog = value);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion
    }
}
