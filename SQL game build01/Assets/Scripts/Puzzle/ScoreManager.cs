using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class ScoreManager : MonoBehaviour
    {
        private int totalScore = 0;

        public event EventHandler<int> OnScoreUpdated;

        public void AddScore(int score)
        {
            totalScore += score;

            OnScoreUpdated?.Invoke(this, totalScore);
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