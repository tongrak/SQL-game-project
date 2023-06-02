using Gameplay.Helper;
using Gameplay.UI.Elements;
using Gameplay.UI.Elements.Puzzle;
using System;
using UnityEngine;

namespace Gameplay.UI.Mode
{
    public class PuzzleModeStarter : CMStarterUnit
    {
        public GameUIMode mode => GameUIMode.PuzzleMode;

        private PuzzleModeController _puzzleConsoleController;
        private ExcuteButtonHandler _exeHandler;

        public PuzzleModeStarter(PuzzleModeController puzzleConsoleController, ExcuteButtonHandler exeHandler)
        {
            _puzzleConsoleController = puzzleConsoleController;
            _exeHandler = exeHandler;
        }

        public void StartUnit(EventHandler modeChangesHandler)
        {
            _puzzleConsoleController.ConsoleModeChanged += modeChangesHandler;
            _puzzleConsoleController.SubOrUnSubToConsole(_exeHandler, true);
            _puzzleConsoleController.ShowMode();
        }

        public void StopUnit(EventHandler modeChangesHandler)
        {
            _puzzleConsoleController.ConsoleModeChanged -= modeChangesHandler;
            _puzzleConsoleController.SubOrUnSubToConsole(_exeHandler, false);
            _puzzleConsoleController.HideMode();
        }
    }

    public class PuzzleModeController : ConsoleModeController
    {
        //UI Object
        [SerializeField] private PuzzleConsoleController _consoleController;
        [SerializeField] private QuestBarController _questBarController;
        //Dynamic Var

        public override void InitMode()
        {
            if (_consoleController == null || _questBarController == null)
            {
                try
                {
                    _consoleController = ComponentHelper.GetObjectWithType<PuzzleConsoleController>();
                    _questBarController = ComponentHelper.GetObjectWithType<QuestBarController>();
                }
                catch (System.Exception e)
                {
                    throw new System.Exception(e.Message);
                }
            }
        }

        public override void ShowMode()
        {
            _consoleController.ShowConsole();
        }

        public void DisplayOutputTable(string[][] data) =>
            _consoleController.DisplayOutput(data);

        public void SubOrUnSubToConsole(ExcuteButtonHandler handler, bool toSub)
        {
            if (toSub) _consoleController.ExcutionCalled += handler;
            else _consoleController.ExcutionCalled -= handler;
        }

        public override void HideMode() => _consoleController.isShow = _questBarController.isShow = false; //Hide the Console and quest bar
    }
}