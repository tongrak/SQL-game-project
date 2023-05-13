using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ConsoleGeneral
{
    public class DialogConsoleMaster : ConsoleBasic
    {
        [Header("Display Element")]
        [SerializeField] private TextMeshProUGUI _titleElement;
        [SerializeField] private TextMeshProUGUI _mainDialogElement;
        [Header("Display Variable")]
        [SerializeField] private string _dialogTitle = "//Title//";
        [SerializeField] private string[] _dialogs;
        [Header("Configure Option")]
        [SerializeField] private bool _IsTyped = false;
        [SerializeField] private int _TypingSlowness = 1;
        
        private int _dialogIndex = 0;

        private bool _hideTitle = false;

        public void ShowDialog()
        {
            if (_hideTitle)
            {
                ShowDialog(_dialogs);
            }
            else ShowDialog(_dialogTitle, _dialogs);
        }

        public void ShowDialog(string title, string[] dialogs)
        {
            //Update displaying var
            _dialogTitle = title;
            _dialogs = dialogs;

            _hideTitle = false;

            SettingUp(_hideTitle);
        }

        public void ShowDialog(string[] dialogs)
        {
            _dialogs = dialogs;

            _hideTitle = true;

            SettingUp(_hideTitle);
        }

        public void ShowNextDialog()
        {
            if (!ChangeDialogBaseOnCurrIndex(1)) Debug.LogWarning("Last dialog displayed");
        }

        public void ShowPreviousDialog()
        {
            if (!ChangeDialogBaseOnCurrIndex(-1)) Debug.LogWarning("No negetive indexed dialog existed");
        }

        #region private functions
        private void SettingUp(bool hideTitle)
        {
            //Settign config vars
            _dialogIndex = 0;

            //clear elements, show title and dialog console
            ClearTitleNDialog();
            //ToHideTitle(hideTitle);
            ToHide(false);

            //enforce change
            if (!hideTitle)
            {
                Debug.Log("Enter UpdateTitle");
                UpdateTitle();
            }
            UpdateDialog();
        }

        private bool ChangeDialogBaseOnCurrIndex(int changes)
        {
            int nextIndex = _dialogIndex + changes;
            if (nextIndex >= 0 && nextIndex < _dialogs.Length)
            {
                _dialogIndex = nextIndex;
                UpdateDialog();
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

        private void ToHideTitle(bool toHide)
        {
            if (toHide) { _titleElement.text = string.Empty; }
        }

        #endregion

        #region Unity Basics
        /*private void Awake()
        {
            ToHide(true);
        }*/

        #endregion
    }
}


