using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PuzzleController
{
    public class QueryPuzzleGetKeyItemController : MonoBehaviour, IPuzzleController
    {
        #region Interface's properties
        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.QueryPuzzleGetKeyItem;
        public string[] PrePuzzleDialog => throw new Exception("This puzzle doesn't have dialog for using key item.");
        public string[] PuzzleDialog { get; protected set; }
        public string QueryQuestion { get; protected set; }
        public string[] ConditionMessage { get; protected set; }
        public PuzzleResult CurrPuzzleResult { get; protected set; }
        public bool IsLock => throw new Exception("This puzzle doesn't have to use key item to unlock.");
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
            return queryPControl.GetResult(playerQuery);
        }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            throw new Exception("This puzzle doesn't need key item to unlock");
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
