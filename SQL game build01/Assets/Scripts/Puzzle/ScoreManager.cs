using System;
using UnityEngine;

namespace Puzzle
{
    public class ScoreManager : MonoBehaviour
    {
        private int totalScore = 0;

        public event EventHandler<int> OnTotalScoreUpdated;

        public void AddScore(int score)
        {
            totalScore += score;

            OnTotalScoreUpdated?.Invoke(this, totalScore);
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