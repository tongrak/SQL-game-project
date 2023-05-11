using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGenerals
{
    public class ConsolesMaster : MonoBehaviour
    {
        [System.NonSerialized] private ConsoleMode _currentMode;

        private PuzzleConsoleMaster _puzzleConsole;
        private DialogConsoleMaster _dialogConsole;

        private bool _allConsoleLoaded = false;

        //acquire SQL/Puzzle Master

        public void ShowConsole(ConsoleMode console)
        {
            HideAllConsole();
            switch (console)
            {
                case ConsoleMode.ExploreMode: throw new NotImplementedException("Explore console isn't implemented");
                case ConsoleMode.PuzzleMode: _puzzleConsole.ToHide(false); break;
                case ConsoleMode.DialogMode: _dialogConsole.ToHide(false); break;
            }
        }

        public void GetPuzzleResponse(string playerInput)
        {
            //SQL/Puzzle Master.GetResponse(playerResponse)
            Debug.Log("Player Input: " + playerInput);
        }

        private bool PuzzleConsoleInit()
        {
            _puzzleConsole = GameObject.Find("Puzzle Console").GetComponent<PuzzleConsoleMaster>();
            if (_puzzleConsole != null)
            {
                _puzzleConsole.ExcutionCalled += GetPuzzleResponse;
                return true;
            }
            return false;
        }

        private bool DialogConsoleInit()
        {
            _dialogConsole = GameObject.FindFirstObjectByType<DialogConsoleMaster>();
            if (_dialogConsole != null) return true;
            return false;
        }

        private void HideAllConsole()
        {
            _puzzleConsole?.ToHide(true);
            _dialogConsole?.ToHide(true);
        }


        #region UnityBasics

        private void Update()
        {
            //connect all console
            if (!_allConsoleLoaded)
            {
                if (PuzzleConsoleInit() && DialogConsoleInit())
                {
                    ShowConsole(ConsoleMode.PuzzleMode); _allConsoleLoaded = true;
                }
            }
        }
        #endregion
    }
}

