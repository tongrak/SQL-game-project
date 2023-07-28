using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.PuzzleController
{
    public class LockByScoreQueryPuzzleController : MonoBehaviour, IPuzzleControllerOld
    {
        public PuzzleType PuzzleType => throw new System.NotImplementedException();

        public string[] PrePuzzleDialog => throw new System.NotImplementedException();

        public string[] PuzzleDialog => throw new System.NotImplementedException();

        public string QueryQuestion => throw new System.NotImplementedException();

        public string[] ConditionMessage => throw new System.NotImplementedException();

        public PuzzleResult CurrPuzzleResult => throw new System.NotImplementedException();

        public bool IsLock => throw new System.NotImplementedException();

        public int GetExecutedNum()
        {
            throw new System.NotImplementedException();
        }

        public KeyItem GetKeyItem()
        {
            throw new System.NotImplementedException();
        }

        public PuzzleResult GetResult(string playerQuery)
        {
            throw new System.NotImplementedException();
        }

        public bool InsertKeyItem(KeyItem playerItem)
        {
            throw new System.NotImplementedException();
        }

        public void ResetExecutedNum()
        {
            throw new System.NotImplementedException();
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