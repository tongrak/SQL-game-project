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

        public IHaveItem HaveItemCom { get; private set; }
        public IFinalPuzzle FinalPuzzleCom { get; private set; }
        public ILockerItem LockerItemCom { get; private set; }
        public ILockerScore LockerScoreCom { get; private set; }
        public IQueryPuzzle QueryPuzzleCom { get; private set; }

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