using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGeneral
{
    public class ConsolesMaster : MonoBehaviour
    {
        [SerializeField] private ConsoleMode _defaultMode = ConsoleMode.ExploreMode;
        private ConsoleMode _currentMode;
        //Dumb field
        private bool _allConsoleLoaded = false;
        //Submaster controller
        private PuzzleConsoleMaster _puzzleConsole;
        private DialogConsoleMaster _dialogConsole;
        private QuestBarMaster _questBarConsole;
        //Dynamic field
        private PuzzleMaster _currPuzzle;
        private Queue<ConModeStarterUnit> _consoleOrder = new Queue<ConModeStarterUnit>();

        public void ShowConsole(ConsoleMode console)
        {
            HideAllConsole();
            switch (console)
            {
                case ConsoleMode.ExploreMode: throw new NotImplementedException("Explore console isn't implemented");
                case ConsoleMode.PuzzleMode: _puzzleConsole.isShow = true; break;
                case ConsoleMode.DialogMode: _dialogConsole.isShow = true; break;
            }
        }
        public void ShowConsoleFor(PuzzleMaster pm)
        {
            _currPuzzle = pm;
            //ShowConsole(ConsoleMode.DialogMode);
            //add into dialog 
            this._consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogConsole, pm.Dialog, null));
            //switch pm.puzzletype -> each puzzle show console in different order
            //current puzzletype = dialogThenPuzzle
            this._consoleOrder.Enqueue(CMStarterFactory.CreatePuzzleMode(_puzzleConsole));

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
                _dialogConsole.DialogConfirmation += ToNextConsole;
                Debug.Log("Console: Dialog connected");
                return true;
            }
            return false;
        }
        private void ToNextConsole()
        {
            //base on puzzle type
            ConModeStarterUnit nextConsole;
            if(_consoleOrder.TryDequeue(out nextConsole)) nextConsole.StartConsole();
            else ShowConsole(_defaultMode);
        }
        #endregion

        #region QuestBar Control
        /*
        public void ShowQuestBar()
        {
            _questBarConsole.isShow = true;
        }*/

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
            _puzzleConsole.isShow = false;
            _dialogConsole.isShow = false;
        }
        private void SetConsoleOrder(PuzzleType type)
        {
            switch (type)
            {
                case PuzzleType.query: break;
                case PuzzleType.keyItem: break;
                case PuzzleType.queryAndKeyItem: break;
                default:
                    this._consoleOrder.Clear();
                break;
            }
        }
        #endregion

        #region UnityBasics

        private void Awake()
        {
            _currentMode = _defaultMode;
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
                    _questBarConsole.isShow = false;
                    _allConsoleLoaded = true;
                }
            }
        }
        #endregion
    }
}

