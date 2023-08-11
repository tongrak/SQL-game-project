using Assets.Scripts.Puzzle.PuzzleController.Interface;
using Puzzle;
using System;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    public class LockerScore : MonoBehaviour, ILockerScore
    {
        [field: SerializeField] public int ScoreUse { get; private set; }

        private ScoreManager scoreManager;

        public bool IsLocked { get; private set; } = true;

        public event EventHandler OnUnlocked;

        // Use this for initialization
        void Start()
        {
            scoreManager = GetComponent<PuzzleController>().ScoreManager;
            scoreManager.OnTotalScoreUpdated += CheckScore;
        }

        private void CheckScore(object sender, int totalScore)
        {
            if(totalScore >= ScoreUse)
            {
                // Unlock
                IsLocked = false;
                OnUnlocked?.Invoke(this, EventArgs.Empty);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}