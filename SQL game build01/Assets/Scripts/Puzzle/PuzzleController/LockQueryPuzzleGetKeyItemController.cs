using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleController
{
    public class LockQueryPuzzleGetKeyItemController : MonoBehaviour, IPuzzleController
    {
        #region Interface's properties
        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.LockQueryPuzzleGetKeyItem;
        public string[] PrePuzzleDialog { get; protected set; }
        public string[] PuzzleDialog { get; protected set; }
        public string QueryQuestion { get; protected set; }
        public string[] ConditionMessage { get; protected set; }
        public PuzzleResult CurrPuzzleResult { get; protected set; }
        public bool IsLock { get; protected set; }
        #endregion

        [SerializeField] QueryPuzzleControllerParent queryPControl = new QueryPuzzleControllerParent();
        [SerializeField] LockPuzzleControllerParent lockPControl = new LockPuzzleControllerParent();
        [SerializeField] GetItemPuzzleControllerParent getItemPControl = new GetItemPuzzleControllerParent();

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
            return lockPControl.InsertKeyItem(playerItem, value => IsLock = value);
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
