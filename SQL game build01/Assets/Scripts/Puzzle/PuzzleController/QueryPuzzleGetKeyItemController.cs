using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Puzzle.PuzzleController
{
    public class QueryPuzzleGetKeyItemController : MonoBehaviour, IPuzzleController
    {
        #region Interface's properties
        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.QueryPuzzleGetKeyItem;
        public string[] PrePuzzleDialog => throw new Exception(PuzzleControlExceptionMessage.noPrePuzzleDialog);
        public string[] PuzzleDialog { get; protected set; }
        public string QueryQuestion { get; protected set; }
        public string[] ConditionMessage { get; protected set; }
        public PuzzleResult CurrPuzzleResult { get; protected set; }
        public bool IsLock => throw new Exception(PuzzleControlExceptionMessage.noIsLock);
        #endregion

        [SerializeField] protected QueryPuzzleControllerParent queryPControl = new QueryPuzzleControllerParent();
        [SerializeField] protected GetItemPuzzleControllerParent getItemPControl = new GetItemPuzzleControllerParent();

        #region Interface's methods
        public int GetExecutedNum()
        {
            return queryPControl.GetExecutedNum();
        }

        public KeyItem GetKeyItem()
        {
            return getItemPControl.GetKeyItem();
        }

        public PuzzleResult GetResult(string playerQuery)
        {
            return queryPControl.GetResult(playerQuery, value => CurrPuzzleResult = value);
        }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            throw new Exception(PuzzleControlExceptionMessage.noInsertKeyItemMethod);
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
