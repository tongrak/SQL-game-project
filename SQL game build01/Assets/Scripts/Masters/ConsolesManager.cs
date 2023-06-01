using GameHelper;
using Puzzle;
using Puzzle.PuzzleController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGeneral
{
    public class ConsolesManager : MonoBehaviour
    {
        [SerializeField] private ConsoleMode _defaultMode = ConsoleMode.ExploreMode;

        private ExploreModeController _exploreMode;
        private PuzzleModeController _puzzleMode;
        private DialogModeController _dialogMode;

        //Dynamic field
        private ConsoleModeController[] _modeControllers;
        private IPuzzleController _currPuzzle;
        private Queue<CMStarterUnit> _consoleOrder = new Queue<CMStarterUnit>();
        private CMStarterUnit _currUnit;

        public void ShowConsoleFor(IPuzzleController pm)
        {
            _currPuzzle = pm;
            //add into dialog 
            //if (pm.PrePuzzleDialog == null) this._consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogConsole, pm.PuzzleDialog, null));
            //else this._consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogConsole, pm.PrePuzzleDialog, null));
            this._consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogMode, pm.PuzzleDialog, null));
            //switch pm.puzzletype -> each puzzle show console in different order
            //current puzzletype = dialogThenPuzzle
            this._consoleOrder.Enqueue(CMStarterFactory.CreatePuzzleMode(_puzzleMode, GetPuzzleResponse));

            ToNextConsole();
        }

        #region PuzzleMode Control

        public void GetPuzzleResponse(string playerInput)
        {
            Debug.Log("Player Input: " + playerInput);
            PuzzleResult result = _currPuzzle.GetResult(playerInput);
            if (!result.isError) _puzzleMode.DisplayOutputTable(result.queryResult);
            else Debug.LogWarning(string.Format("Input error:{0}", result.errorMessage));
        }

        #endregion

        #region Misc Function
        private void HideAllMode()
        {
            foreach (ConsoleModeController cmc in _modeControllers) if (cmc != null) cmc.HideMode();
        }
        private void ForceShowMode(ConsoleMode mode)
        {
            _consoleOrder.Clear();
           switch (mode)
           {
                case ConsoleMode.ExploreMode: _consoleOrder.Enqueue(CMStarterFactory.CreateExploreMode()); break;
                case ConsoleMode.PuzzleMode: _consoleOrder.Enqueue(CMStarterFactory.CreatePuzzleMode(_puzzleMode, GetPuzzleResponse)); break;
                case ConsoleMode.DialogMode: _consoleOrder.Enqueue(CMStarterFactory.CreateDialogMode(_dialogMode, null, null)); break;
           }
            ToNextConsole();
        }

        private void ToNextConsole()
        {
            //HideAllConsole();
            if (_currUnit != null) _currUnit.StopUnit(ToNextConsole); //Stop current unit
            if (_consoleOrder.TryDequeue(out _currUnit)) _currUnit.StartUnit(ToNextConsole); //if there mode in queue start the lastest
            else ForceShowMode(_defaultMode); //else force to default mode
        }
        private void ToNextConsole(System.Object sender, EventArgs args) => ToNextConsole();
        #endregion

        #region UnityBasics

        private void Start()
        {
            //Start with default mode
            //_currentMode = _defaultMode;

            try
            {
                //_exploreMode = ComponentHelper.GetObjectWithType<ExploreModeController>();
                _puzzleMode = ComponentHelper.GetObjectWithType<PuzzleModeController>();
                _dialogMode = ComponentHelper.GetObjectWithType<DialogModeController>();
            }
            catch(FailToGetUnityObjectException fgo)
            {
                Debug.LogException(fgo);
                //Handling above object initiate failure
                //Somehow....
                throw new System.Exception(fgo.Message);
            }
            Debug.Log("Console: init complete");
            ConsoleModeController[] temp = { null, _puzzleMode, _dialogMode };
            _modeControllers = temp;
            //set up
            HideAllMode();
            ForceShowMode(_defaultMode);
        }
        #endregion
    }
}