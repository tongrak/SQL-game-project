using Assets.Scripts.Puzzle.PuzzleController.Interface;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    public class RequiredPuzzle : MonoBehaviour, IRequiredPuzzle
    {
        public bool IsLock { get; private set; } = true;

        public void UnLock()
        {
            IsLock = false;
        }

        private void Awake()
        {
            GetComponent<PuzzleController>().RequiredPuzzleManager.RequiredPuzzles.Add(this);
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