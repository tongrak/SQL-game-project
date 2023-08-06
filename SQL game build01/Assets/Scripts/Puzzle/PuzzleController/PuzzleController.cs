using Assets.Scripts.Puzzle.PuzzleController.Interface;
using Puzzle;
using Puzzle.PuzzleController.Interface;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    public class PuzzleController : MonoBehaviour, IPuzzleController
    {
        [field: SerializeField] public PuzzleType PuzzleType { get; private set; }
        [field: SerializeField] public ScoreManager ScoreManager { get; private set; }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}