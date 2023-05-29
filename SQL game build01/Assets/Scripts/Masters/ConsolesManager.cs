using GameHelper;
using PuzzleController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGeneral
{
    public class ConsolesManager : MonoBehaviour
    {
        [SerializeField] private ConsoleMode _defaultMode = ConsoleMode.ExploreMode;
        private ConsoleMode _currentMode;
        //Submaster controller
        private PuzzleConsoleMaster _puzzleConsole;
        private DialogConsoleController _dialogConsole;
        private QuestBarMaster _questBarConsole;

        private DialogModeController _dialogMode;

        //Dynamic field
        private IPuzzleController _currPuzzle;
        private Queue<CMStarterUnit> _consoleOrder = new Queue<CMStarterUnit>();

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
        public void ShowConsoleFor(IPuzzleController pm)
        {
            _currPuzzle = pm;
            //add into dialog 
            //if (pm.PrePuzzleDialog == null) this._consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogConsole, pm.PuzzleDialog, null));
            //else this._consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogConsole, pm.PrePuzzleDialog, null));
            this._consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogConsole, pm.PuzzleDialog, null));
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
            PuzzleResult result =  _currPuzzle.GetResult(playerInput);
            if (!result.isError)
            {
                _puzzleConsole.DisplayOutput(result.queryResult);
            }
            else Debug.LogWarning(result.errorMessage);
                
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
            CMStarterUnit nextConsole;
            if (_consoleOrder.TryDequeue(out nextConsole)) nextConsole.StartConsole();
            else ShowConsole(_defaultMode);
        }
        #endregion

        #region UnityBasics

        private void Start()
        {
            //_dialogMode = DialogModeController.Instance;

            _currentMode = _defaultMode;
            try
            {
                _puzzleConsole = ComponentHelper.GetObjectWithType<PuzzleConsoleMaster>();
                _dialogConsole = ComponentHelper.GetObjectWithType<DialogConsoleController>();
                _questBarConsole = ComponentHelper.GetObjectWithType<QuestBarMaster>();
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