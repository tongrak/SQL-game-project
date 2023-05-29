using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class QueryPuzzleScoreManager : MonoBehaviour
    {
        private int totalScore = 0;

        public void AddScore(int score)
        {
            totalScore += score;
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