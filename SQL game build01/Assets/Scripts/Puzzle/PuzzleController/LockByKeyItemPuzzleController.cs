using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Puzzle.PuzzleController
{
    public class LockByKeyItemPuzzleController : MonoBehaviour, IPuzzleController
    {
        #region Interface's properties
        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.LockByKeyItemPuzzle;
        public string[] PrePuzzleDialog { get; protected set; }
        public string[] PuzzleDialog { get; protected set; }
        public string QueryQuestion { get; protected set; }
        public string[] ConditionMessage => throw new Exception(PuzzleControlExceptionMessage.noConditionMessgage);
        public PuzzleResult CurrPuzzleResult => throw new Exception(PuzzleControlExceptionMessage.noCurrPuzzleResult);
        public bool IsLock { get; protected set; } = false;
        #endregion

        [SerializeField] protected LockByKeyItemPuzzleControllerParent lockPControl = new LockByKeyItemPuzzleControllerParent();

        #region Interface's methods
        public PuzzleResult GetResult(string playerQuery)
        {
            throw new Exception(PuzzleControlExceptionMessage.noGetResultMethod);
        }

        public KeyItem GetKeyItem()
        {
            throw new Exception(PuzzleControlExceptionMessage.noGetKeyItemMethod);
        }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            return lockPControl.InsertKeyItem(playerItem, value => IsLock = value);
        }

        public void ResetExecutedNum()
        {
            throw new Exception(PuzzleControlExceptionMessage.noResetExecutedNumMethod);
        }

        public int GetExecutedNum()
        {
            throw new Exception(PuzzleControlExceptionMessage.noGetExecutedNumMethod);
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
