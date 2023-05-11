using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGeneral
{
    public class ConsolesMaster : MonoBehaviour
    {
        [System.NonSerialized] private ConsoleMode _currentMode;

        private PuzzleConsoleMaster _puzzleConsole;
        private DialogConsoleMaster _dialogConsole;
        private QuestBarMaster _questBarConsole;

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

        public void ShowQuestBar(string quest)
        {
            _questBarConsole.ShowQuestBar(quest);
        }

        #region PuzzleConsole Control
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
        public void GetPuzzleResponse(string playerInput)
        {
            //SQL/Puzzle Master.GetResponse(playerResponse)
            Debug.Log("Player Input: " + playerInput);
        }
        #endregion

        #region DialogConsole Control
        private bool DialogConsoleInit()
        {
            _dialogConsole = GameObject.FindFirstObjectByType<DialogConsoleMaster>();
            if (_dialogConsole != null) return true;
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
        private void Update()
        {
            //connect all console
            if (!_allConsoleLoaded)
            {
                if (PuzzleConsoleInit() && DialogConsoleInit() && QuestBarInit())
                {
                    ShowConsole(ConsoleMode.PuzzleMode); 
                    _allConsoleLoaded = true;
                }
            }
        }
        #endregion
    }
}

