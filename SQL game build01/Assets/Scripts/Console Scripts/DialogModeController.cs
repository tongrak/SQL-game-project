using GameHelper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGeneral 
{
    public delegate void DialogConfirmationHandler();

    public class DialogModeStarter : CMStarterUnit
    {
        private DialogConsoleController _dialogConsoleController;
        private string[] _rawDialogs;
        private string _confirmMessage = null;

        public DialogModeStarter(DialogConsoleController dialogConsoleController, string[] rawDialogs, string confirmMessage)
        {
            _dialogConsoleController = dialogConsoleController;
            _rawDialogs = rawDialogs;
            _confirmMessage = confirmMessage;
        }

        public void StartConsole()
        {
            if (string.IsNullOrEmpty(_confirmMessage)) _dialogConsoleController.ShowConsole(_rawDialogs);
            else _dialogConsoleController.ShowConsole(_rawDialogs, _confirmMessage);
        }
    }

    public class DialogModeController : ConsoleModeController
    {
        [SerializeField] private DialogConsoleController _consoleController;
        //private event DialogConfirmationHandler _dialogConfirmed;

        public override void InitMode()
        {
            _consoleController = ComponentHelper.GetObjectWithType<DialogConsoleController>();
        }

        public override void ShowMode()
        {
            _consoleController.ShowConsole();
        }

        public void ShowMode(string[] rawDialogs)
        {
            _consoleController.ShowConsole(rawDialogs);
        }

        public void ShowMode(string[] rawDialogs, string confirmMessage)
        {
            _consoleController.ShowConsole(rawDialogs, confirmMessage);
        }

        public override void HideMode()
        {
            _consoleController.isShow = false;
        }

        private void testFunction()
        {
            this.RaiseModeChanged(this);
        }
    }
}



