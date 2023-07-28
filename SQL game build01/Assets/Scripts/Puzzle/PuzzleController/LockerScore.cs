using Assets.Scripts.Puzzle.PuzzleController.Interface;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    public class LockerScore : MonoBehaviour, ILockerScore
    {
        [field: SerializeField] public int ScoreUse { get; private set; }

        public bool IsLocked { get; private set; } = true;

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