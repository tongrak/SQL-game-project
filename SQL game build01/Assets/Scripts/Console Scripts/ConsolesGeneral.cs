
using System;
using TMPro;
using UnityEngine;

namespace ConsoleGeneral
{
    public delegate void ExcuteButtonHandler(string playerInput);

    public enum ConsoleMode { ExploreMode, PuzzleMode, DialogMode}

    #region Console Mode Unit
    public static class CMStarterFactory
    {
        public static CMStarterUnit CreateDialogMode(DialogModeController dialogConsole, string[] dialogs, string confirmMessage)
                => new DialogModeStarter(dialogConsole, dialogs, confirmMessage);

        public static CMStarterUnit CreatePuzzleMode(PuzzleConsoleMaster puzzleConsole)
               => new PuzzleConsoleStarter(puzzleConsole);
    }

    public interface CMStarterUnit
    {
        public void StartConsole();
    }
    #endregion

    public class SingletonClass<T> where T : class, new() 
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = new T();
                return _instance;
            }
        }
    }

    public class UIElementBasic : MonoBehaviour
    {
        public bool isShow
        {
            get => this.gameObject.activeSelf;
            set => this.gameObject.SetActive(value);
        }
    }

    #region Consoles abstracts & interfaces
    public abstract class ConsoleBasic : UIElementBasic
    {
        public abstract void ShowConsole();
    }

    public class ConsoleModeController
    {
        public event EventHandler ConsoleModeChanged;
        protected void RaiseModeChanged(System.Object sender)
        {
            this.ConsoleModeChanged?.Invoke(sender, EventArgs.Empty);
        }

        public virtual void InitMode() { }
        public virtual void ShowMode() { }
        public virtual void HideMode() { }
    }
    #endregion

    #region Consoles element classes
    public class TextBox : MonoBehaviour
    {
        private string _displayText;
        public string text { get { return _displayText; } }
        [Header("Display element")]
        [SerializeField] private TextMeshProUGUI _textElement;

        public void SetDispalyText(string text)
        {
            _displayText = text;
        }
        public void ShowSelf(bool show)
        {
            this.gameObject.SetActive(show);
        }
        public void ShowSelf(string text)
        {
            SetDispalyText(text);
            UpdateDisplayText();
            ShowSelf(true);
        }
        private void UpdateDisplayText()
        {
            this._textElement.text = _displayText;
        }
        
    }
    #endregion
}