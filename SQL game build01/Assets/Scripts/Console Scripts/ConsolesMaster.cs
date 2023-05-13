using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGeneral
{
    public class ConsolesMaster : MonoBehaviour
    {
        [SerializeField] private ConsoleMode _startingMode = ConsoleMode.ExploreMode;
        private ConsoleMode _currentMode;

        private bool _allConsoleLoaded = false;

        private PuzzleConsoleMaster _puzzleConsole;
        private DialogConsoleMaster _dialogConsole;
        private QuestBarMaster _questBarConsole;

        //acquire SQL/Puzzle Master

        public void ShowConsole(ConsoleMode console)
        {
            HideAllConsole();
            switch (console)
            {
                case ConsoleMode.ExploreMode: throw new NotImplementedException("Explore console isn't implemented");
                case ConsoleMode.PuzzleMode: _puzzleConsole.ToHide(false); break;
                case ConsoleMode.DialogMode: _dialogConsole.ShowDialog(); break;
            }
        }

        public void ShowQuestBar(string quest)
        {
            _questBarConsole.ShowQuestBar(quest);
        }

        #region PuzzleConsole Control
        private bool PuzzleConsoleInit()
        {
            _puzzleConsole = FindAnyObjectByType<PuzzleConsoleMaster>();
            if (_puzzleConsole != null)
            {
                _puzzleConsole.ExcutionCalled += GetPuzzleResponse;
                Debug.Log("Console: Puzzle connected");
                return true;
            }
            return false;
        }
        public void GetPuzzleResponse(string playerInput)
        {
                //Get response from puzzle master
                Debug.Log("Player Input: " + playerInput);
        }
        #endregion

        #region DialogConsole Control
        private bool DialogConsoleInit()
        {
            _dialogConsole = GameObject.FindFirstObjectByType<DialogConsoleMaster>();
            if (_dialogConsole != null)
            {
                Debug.Log("Console: Dialog connected");
                return true;
            }
            return false;
        }
        #endregion

        #region QuestBar Control

        public void ShowQuestBar()
        {
            _questBarConsole.ToHide(false);
        }

        private bool QuestBarInit()
        {
            _questBarConsole = FindAnyObjectByType<QuestBarMaster>();
            if (_questBarConsole != null) return true;
            return false;
        }
        #endregion

        #region Misc Function
        private void HideAllConsole()
        {
            _puzzleConsole?.ToHide(true);
            _dialogConsole?.ToHide(true);
        }

        #endregion

        #region UnityBasics

        private void Awake()
        {
            _currentMode = _startingMode;
        }

        private void Update()
        {
            //connect all console
            if (!_allConsoleLoaded)
            {
                if (PuzzleConsoleInit() && DialogConsoleInit() && QuestBarInit())
                {
                    Debug.Log("Console: All set");
                    ShowConsole(_currentMode); 
                    _questBarConsole.ToHide(true);
                    _allConsoleLoaded = true;
                }
            }
        }
        #endregion
    }
}

