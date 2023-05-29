using GameHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleGeneral
{
    public class DialogConsoleController : ConsoleBasic
    {
        [Header("Display Element")]
        [SerializeField] private TextMeshProUGUI _titleElement;
        [SerializeField] private TextMeshProUGUI _mainDialogElement;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private ConfirmButton _confirmButton;
        [Header("Display Variable")]
        [SerializeField] private string _dialogTitle = "//Title//";
        //[SerializeField] private string _dialog = "//Dialog//";
        [SerializeField] private string _confirmText = "confirm";
        [Header("Configure Option")]
        [SerializeField] private bool _IsTyped = false;
        [SerializeField] private int _TypingSlowness = 1;
        //Dynamic fields
        public event DialogConfirmationHandler DialogConfirmation;
        private string[] _rawDialogs = null; //combination of title and dialog
        private int _dialogIndex = 0;

        //Showing function
        public override void ShowConsole()
        {
            this.isShow = true;
            UpdateDialogDisplay();
            UpdateButtons();
        }
        public void ShowConsole(string[] rawDialogs, string confirmMessage)
        {
            this._confirmText = confirmMessage;
            ShowConsole(rawDialogs);
        }
        public void ShowConsole(string[] rawDialogs)
        {
            this._rawDialogs = rawDialogs;
            this._dialogIndex = 0;
            ShowConsole();
        }

        #region UI elements function
        public void ShowNextDialog()
        {
            if (!ChangeDialogBaseOnCurrIndex(1)) Debug.LogWarning("Last dialog displayed");
        }
        public void ShowPreviousDialog()
        {
            if (!ChangeDialogBaseOnCurrIndex(-1)) Debug.LogWarning("No negetive indexed dialog existed");
        }
        public void ConfirmButtonAct()
        {
            ConfirmationCall();
        }
        public virtual void ConfirmationCall()
        {
            DialogConfirmation?.Invoke();
        }
        #endregion

        #region private functions
        private bool ChangeDialogBaseOnCurrIndex(int changes)
        {
            int nextIndex = _dialogIndex + changes;
            if (nextIndex >= 0 && nextIndex < _rawDialogs.Length)
            {
                this._dialogIndex = nextIndex;
                ShowConsole();
                return true;
            }
            else return false;
        }
        private void ClearTitleNDialog()
        {
            _titleElement.text = string.Empty;
            _mainDialogElement.text = string.Empty;
        }
        private void DisplayTitle(bool showTitle)
        {
            this._titleElement.text = (showTitle) ? _dialogTitle : string.Empty;
        }
        private void UpdateTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                _dialogTitle = title;
                DisplayTitle(true);
            }
            else DisplayTitle(false);

        }
        private void UpdateDialogDisplay()
        {
            string rawDialog = _rawDialogs[_dialogIndex];
            Tuple<string, string> titleNDialog = StringHelper.SpliteByPivot(" : ", rawDialog);
            //if raw dialog can be splited display title else "hide" it
            UpdateTitle((titleNDialog != null) ? titleNDialog.Item1 : null); 
            string displayDialog = (titleNDialog != null) ? titleNDialog.Item2 : rawDialog;

            if (_IsTyped)
            {
                StopAllCoroutines();
                StartCoroutine(TypeDialog(displayDialog));
            }else _mainDialogElement.text = displayDialog;

        }
        private IEnumerator TypeDialog(string dialog)
        {
            _mainDialogElement.text = string.Empty;
            foreach(char letter in dialog.ToCharArray())
            {
                _mainDialogElement.text += letter;

                //Wait for determined typing slowness frames
                float waitFrame = Time.deltaTime * _TypingSlowness;
                yield return new WaitForSeconds(waitFrame);
            }
        }
        private void UpdateButtons()
        {
            //Previous and Next buttons
            _previousButton.enabled = (_dialogIndex == 0) ? false : true;
            bool isLastDialog = (_dialogIndex == _rawDialogs.Length - 1) ? true : false;
            _nextButton.enabled = !isLastDialog;
            //ConfirmButton;
            _confirmButton.SetDispalyText(_confirmText);
            _confirmButton.ShowSelf(isLastDialog);
        }
        #endregion
    }
}


