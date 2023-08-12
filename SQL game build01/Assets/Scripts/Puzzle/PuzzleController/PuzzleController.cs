using Assets.Scripts.Puzzle.PuzzleController.Interface;
using Puzzle;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    public class PuzzleController : MonoBehaviour, IPuzzleController
    {
        [field: SerializeField] public PuzzleType PuzzleType { get; private set; }
        [field: SerializeField] public ScoreManager ScoreManager { get; private set; }
        [field: SerializeField] public RequiredPuzzleManager RequiredPuzzleManager { get; private set; }

        private QueryPuzzle queryPuzzle;
        private LockerItem lockerItem;
        private LockerScore lockerScore;
        private RequiredPuzzle requiredPuzzle;

        private bool?[] IsLockedLockers; // [0] = query, [1] = Item, [2] = score 

        private void Initiate()
        {
            IsLockedLockers = new bool?[3];

            queryPuzzle = GetComponent<QueryPuzzle>();
            if(queryPuzzle != null ) 
            { 
                IsLockedLockers[0] = true;
                queryPuzzle.OnQueryCorrect += UnlockQuery;
            }

            lockerItem = GetComponent<LockerItem>();
            if(lockerItem != null ) 
            { 
                IsLockedLockers[1] = true;
                lockerItem.OnUnlocked += UnlockItem;
            }

            lockerScore = GetComponent<LockerScore>();
            if(lockerScore != null ) 
            { 
                IsLockedLockers[2] = true;
                lockerScore.OnUnlocked += UnlockScore;   
            }

            requiredPuzzle = GetComponent<RequiredPuzzle>();
        }

        private void UnlockTheRequired()
        {
            if( IsLockedLockers.Count(x => x == true) == 0 )
            {
                requiredPuzzle?.UnLock();
            }
        }

        #region Method for events
        private void UnlockScore(object sender, EventArgs e)
        {
            IsLockedLockers[2] = false;
            lockerScore.OnUnlocked -= UnlockScore;

            UnlockTheRequired();
        }

        private void UnlockItem(object sender, EventArgs e)
        {
            IsLockedLockers[1] = false;
            lockerItem.OnUnlocked -= UnlockItem;

            UnlockTheRequired();
        }

        private void UnlockQuery(object sender, EventArgs e)
        {
            IsLockedLockers[0] = false;
            queryPuzzle.OnQueryCorrect -= UnlockQuery;

            UnlockTheRequired();
        }
        #endregion

        // Use this for initialization
        void Start()
        {
            Initiate();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}