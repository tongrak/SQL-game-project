using Assets.Scripts.Puzzle.PuzzleController.Interface;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    public class FinalPuzzle : MonoBehaviour, IFinalPuzzle
    {
        public bool IsLock { get; private set; } = true;

        public void UnLock()
        {
            IsLock = false;
        }

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