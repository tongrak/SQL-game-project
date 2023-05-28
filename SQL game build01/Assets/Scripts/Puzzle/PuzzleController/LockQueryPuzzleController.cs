using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Puzzle.PuzzleController
{
    public class LockQueryPuzzleController : MonoBehaviour, IPuzzleController
    {
        #region Interface's properties
        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.LockQueryPuzzle;
        public bool IsLock { get; protected set; } = false;
        public string[] PrePuzzleDialog { get; protected set; }
        public string[] PuzzleDialog { get; protected set; }
        public string QueryQuestion { get; protected set; }
        public string[] ConditionMessage { get; protected set; }
        public PuzzleResult CurrPuzzleResult { get; protected set; }
        #endregion

        [SerializeField] protected QueryPuzzleControllerParent queryPControl = new QueryPuzzleControllerParent();
        [SerializeField] protected LockPuzzleControllerParent lockPControl = new LockPuzzleControllerParent();

        #region Interface's methods
        public PuzzleResult GetResult(string playerQuery)
        {
            return queryPControl.GetResult(playerQuery, value => CurrPuzzleResult = value);
        }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            return lockPControl.InsertKeyItem(playerItem, value => IsLock = value);
        }
        public int GetExecutedNum()
        {
            return queryPControl.GetExecutedNum();
        }

        public KeyItem GetKeyItem()
        {
            throw new Exception(PuzzleControlExceptionMessage.noGetKeyItemMethod);
        }

        public void ResetExecutedNum()
        {
            queryPControl.ResetExecutedNum();
        }
        #endregion

        #region Unity's methods
        void Awake()
        {
            queryPControl.Load_QueryPuzzle(value => PuzzleDialog = value, value => QueryQuestion = value, value => ConditionMessage = value, value => CurrPuzzleResult = value);
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