using GameHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGeneral 
{
    public delegate void DialogConfirmationHandler();

    public class DialogModeStarter : CMStarterUnit
    {
        public ConsoleMode mode => ConsoleMode.DialogMode;

        private DialogModeController _dialogConsoleController;
        private string[] _rawDialogs;
        private string _confirmMessage = null;

        public DialogModeStarter(DialogModeController dialogConsoleController, string[] rawDialogs, string confirmMessage)
        {
            _dialogConsoleController = dialogConsoleController;
            _rawDialogs = rawDialogs;
            _confirmMessage = confirmMessage;
        }

        public void StartUnit(EventHandler modeChangesHandler)
        {
            _dialogConsoleController.ConsoleModeChanged += modeChangesHandler;
            if (_rawDialogs == null && string.IsNullOrEmpty(_confirmMessage)) _dialogConsoleController.ShowMode();
            else if (string.IsNullOrEmpty(_confirmMessage)) _dialogConsoleController.ShowMode(_rawDialogs);
            else _dialogConsoleController.ShowMode(_rawDialogs, _confirmMessage);
        }

        public void StopUnit(EventHandler modeChangesHandler)
        {
            _dialogConsoleController.ConsoleModeChanged -= modeChangesHandler;
            _dialogConsoleController.HideMode();
        }
    }

    public class DialogModeController : ConsoleModeController
    {
        [SerializeField] private DialogConsoleController _consoleController;
        //Dynamic Variable

        public override void InitMode()
        {
            if (_consoleController == null) 
            {
                try
                {
                    _consoleController = ComponentHelper.GetObjectWithType<DialogConsoleController>();
                }
                catch (System.Exception e)
                {
                    throw new System.Exception(e.Message);
                }
            } 
        }

        #region Show Mode
        public override void ShowMode() =>
            _consoleController.ShowConsole();
        public void ShowMode(string[] rawDialogs) =>
            _consoleController.ShowConsole(rawDialogs);
        public void ShowMode(string[] rawDialogs, string confirmMessage) => 
            _consoleController.ShowConsole(rawDialogs, confirmMessage);
        #endregion

        public override void HideMode() => 
            _consoleController.isShow = false;

        public void ConfirmAct() =>
            this.RaiseModeChange(this);

    }
}