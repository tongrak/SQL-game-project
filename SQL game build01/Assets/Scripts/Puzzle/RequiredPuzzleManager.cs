using Assets.Scripts.Puzzle.PuzzleController;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Puzzle
{
    public class RequiredPuzzleManager : MonoBehaviour
    {
        public List<RequiredPuzzle> RequiredPuzzles { get; set; } = new List<RequiredPuzzle>();

        private bool IsLocked { get; set; } = true;

        public bool CheckIsAllUnlocked()
        {
            if (IsLocked)
            {
                if (RequiredPuzzles.Any(x => x.IsLock))
                {
                    return false;
                }
                else
                {
                    // Unlock
                    IsLocked = false;
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void Start()
        {

        }
    }
}
