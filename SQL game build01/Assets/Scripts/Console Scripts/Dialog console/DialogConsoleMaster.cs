using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ConsoleGeneral
{
    //public delegate void DialogConsoleConfirmHandler();

    public class DialogConsoleMaster : ConsoleBasic
    {
        [Header("Display Element")]
        [SerializeField] private TextMeshProUGUI _titleElement;
        [SerializeField] private TextMeshProUGUI _mainDialogElement;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private ConfirmButton _confirmButton;
        [Header("Display Variable")]
        [SerializeField] private string _dialogTitle = "//Title//";
        [SerializeField] private string[] _dialogs;
        [SerializeField] private string _confirmText = "confirm";
        [Header("Configure Option")]
        [SerializeField] private bool _IsTyped = false;
        [SerializeField] private int _TypingSlowness = 1;
        private bool _hideTitle = false;
        //Dynamic fields
        private int _dialogIndex = 0;

        public void ShowDialog()
        {
            if (_hideTitle) ShowDialog(null,_dialogs);
            else ShowDialog(_dialogTitle, _dialogs);
        }
        public void ShowDialog(string title, string[] dialogs, string confirmText)
        {
            _confirmText = confirmText;
            _confirmButton.SetDispalyText(_confirmText);
            ShowDialog(title, dialogs);
        }
        public void ShowDialog(string title, string[] dialogs)
        {
            //Update displaying var
            _dialogs = dialogs;
            if (!string.IsNullOrEmpty(title))
            {
                _dialogTitle = title;
                _hideTitle = false;
            }
            _hideTitle = true;

            SettingUp();
        }
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
            //Do something
        }
        #region private functions
        private void SettingUp()
        {
            //Settign config vars
            _dialogIndex = 0;

            //clear elements, show title and dialog console
            ClearTitleNDialog();
            ToHide(false);

            //enforce change
            if (!_hideTitle)
            {
                Debug.Log("Enter UpdateTitle");
                UpdateTitle();
            }
            UpdateButtons();
            UpdateDialog();
        }
        private bool ChangeDialogBaseOnCurrIndex(int changes)
        {
            int nextIndex = _dialogIndex + changes;
            if (nextIndex >= 0 && nextIndex < _dialogs.Length)
            {
                _dialogIndex = nextIndex;
                UpdateDialog();
                UpdateButtons();
                return true;
            }
            else return false;
        }
        private void ClearTitleNDialog()
        {
            _titleElement.text = string.Empty;
            _mainDialogElement.text = string.Empty;
        }
        private void UpdateTitle()
        {
            _titleElement.text = _dialogTitle;
        }
        private void UpdateDialog()
        {
            string newDialog = _dialogs[_dialogIndex];
            if (_IsTyped)
            {
                StopAllCoroutines();
                StartCoroutine(TypeDialog(newDialog));
            }else _mainDialogElement.text = newDialog;

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
            bool isLastDialog = (_dialogIndex == _dialogs.Length - 1) ? true : false;
            _nextButton.enabled = !isLastDialog;
            //ConfirmButton;
            _confirmButton.ShowSelf(isLastDialog);
        }
        #endregion
    }
}


