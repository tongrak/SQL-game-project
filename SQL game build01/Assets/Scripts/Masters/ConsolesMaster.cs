using MasterGeneral;
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
            //add into dialog 
            this._consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogConsole, pm.Dialog, null));
            //switch pm.puzzletype -> each puzzle show console in different order
            //current puzzletype = dialogThenPuzzle
            this._consoleOrder.Enqueue(CMStarterFactory.CreatePuzzleMode(_puzzleConsole));

            ToNextConsole();
        }
        public void ShowQuestBar(string quest)
        {
            _questBarConsole.ShowConsole(quest);
        }

        #region PuzzleConsole Control
        public void GetPuzzleResponse(string playerInput)
        {
                //Get response from puzzle master
                Debug.Log("Player Input: " + playerInput);
        }
        #endregion

        #region Misc Function
        private void HideAllConsole()
        {
            _puzzleConsole.isShow = false;
            _dialogConsole.isShow = false;
        }
        private void ToNextConsole()
        {
            HideAllConsole();
            ConModeStarterUnit nextConsole;
            if (_consoleOrder.TryDequeue(out nextConsole)) nextConsole.StartConsole();
            else ShowConsole(_defaultMode);
        }
        #endregion

        #region UnityBasics

        private void Start()
        {
            _currentMode = _defaultMode;
            try
            {
                _puzzleConsole = MasterHelper.GetObjectWithType<PuzzleConsoleMaster>();
                _dialogConsole = MasterHelper.GetObjectWithType<DialogConsoleMaster>();
                _questBarConsole = MasterHelper.GetObjectWithType<QuestBarMaster>();
            }
            catch(FailToGetUnityObjectException fgo)
            {
                Debug.LogException(fgo);
                //Handling above object initiate failure
                //Somehow....
                throw new System.Exception(fgo.Message);
            }
            Debug.Log("Console: init complete");
            //set up
            ShowConsole(_currentMode);
            _questBarConsole.isShow = false;
            //add sub controller listener
            _puzzleConsole.ExcutionCalled += GetPuzzleResponse;
            _dialogConsole.DialogConfirmation += ToNextConsole;
        }
        #endregion
    }
}

