using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ConsoleGenerals
{
    public class DialogConsoleMaster : ConsoleBasic
    {
        [Header("Display Element")]
        [SerializeField] private TextMeshProUGUI _titleElement;
        [SerializeField] private TextMeshProUGUI _mainDialogElement;
        [Header("Display Variable")]
        [SerializeField] private string _dialogTitle = "//Title//";
        [SerializeField] private string[] _dialogs;
        //Config var
        private int _dialogIndex = 0;

        public void ShowDialog()
        {
            UpdateTitle();
            UpdateDialog();

            ToHideTitle(false);
            ToHide(false);
        }

        public void ShowDialog(string title, string[] dialogs)
        {
            //Update displaying var
            _dialogTitle = title;
            _dialogs = dialogs;
            //Settign config vars
            _dialogIndex = 0;
            //enforce change
            UpdateTitle();
            UpdateDialog();

            ToHideTitle(false);
            ToHide(false);
        }

        public void ShowDialog(string[] dialogs)
        {
            this.ShowDialog("//Empty//", dialogs);
            ToHideTitle(true);
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

        private void UpdateTitle()
        {
            _titleElement.text = _dialogTitle;
        }

        private void UpdateDialog()
        {
            _mainDialogElement.text = _dialogs[_dialogIndex];
        }
        private void ToHideTitle(bool toHide)
        {
            _titleElement.gameObject.SetActive(!toHide);
        }
        #endregion

        #region Unity Basics
        private void Awake()
        {
            ToHide(true);
        }

        #endregion
    }
}


