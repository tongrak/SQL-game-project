using Puzzle;
using Puzzle.PuzzleController.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    public class LockerItem : MonoBehaviour, ILockerItem
    {
        [field: SerializeField] public string[] Dialog { get; private set; }

        [SerializeField] private List<KeyItem> LockKeyItem;

        private List<KeyItem> leftLockKeyItem;

        public bool IsLocked { get; private set; } = true;

        public event EventHandler OnUnlocked;

        public bool InsertKeyItem(KeyItem playerItem)
        {
            if (leftLockKeyItem.Contains(playerItem))
            {
                leftLockKeyItem.Remove(playerItem);
                if (leftLockKeyItem.Count == 0)
                {
                    // Unlock the puzzle.
                    IsLocked = true;
                    OnUnlocked?.Invoke(this, EventArgs.Empty);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        #region For awake method
        private void Load_LockPuzzle()
        {
            leftLockKeyItem = LockKeyItem;
        }
        #endregion

        void Awake()
        {
            Load_LockPuzzle();
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