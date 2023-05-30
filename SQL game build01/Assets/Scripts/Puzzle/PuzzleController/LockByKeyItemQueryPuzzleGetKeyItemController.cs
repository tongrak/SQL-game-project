using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.PuzzleController
{
    public class LockByKeyItemQueryPuzzleGetKeyItemController : MonoBehaviour, IPuzzleController, IQueryPuzzle
    {
        #region Interface's properties
        public PuzzleType PuzzleType { get; protected set; } = PuzzleType.LockByKeyItemQueryPuzzleGetKeyItem;
        public string[] PrePuzzleDialog { get; protected set; }
        public string[] PuzzleDialog { get; protected set; }
        public string QueryQuestion { get; protected set; }
        public string[] ConditionMessage { get; protected set; }
        public PuzzleResult CurrPuzzleResult { get; protected set; }
        public bool IsLock { get; protected set; }
        #endregion

        [SerializeField] QueryPuzzleControllerParent queryPControl = new QueryPuzzleControllerParent();
        [SerializeField] LockByKeyItemPuzzleControllerParent lockPControl = new LockByKeyItemPuzzleControllerParent();
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
            return queryPControl.GetResult(playerQuery, value => CurrPuzzleResult = value);
        }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            return lockPControl.InsertKeyItem(playerItem, value => IsLock = value);
        }

        public void ResetExecutedNum()
        {
            queryPControl.ResetExecutedNum();
        }

        public int GetCurrScore()
        {
            return queryPControl.GetCurrScore();
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
