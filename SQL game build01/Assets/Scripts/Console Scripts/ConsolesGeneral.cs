
using System;
using TMPro;
using UnityEngine;

namespace ConsoleGeneral
{
    //direct delegate
    public delegate void ExcuteButtonHandler(string playerInput);

    public enum ConsoleMode { ExploreMode, PuzzleMode, DialogMode}

    #region Console Mode Unit
    public static class CMStarterFactory
    {
        public static CMStarterUnit CreateExploreMode() => throw new NotImplementedException();
        public static CMStarterUnit CreateDialogMode(DialogModeController dialogConsole, string[] dialogs, string confirmMessage)
                => new DialogModeStarter(dialogConsole, dialogs, confirmMessage);
        public static CMStarterUnit CreatePuzzleMode(PuzzleModeController puzzleConsole, ExcuteButtonHandler exeHandler)
               => new PuzzleModeStarter(puzzleConsole, exeHandler);
    }

    public interface CMStarterUnit
    {
        public ConsoleMode mode { get; }
        public void StartUnit(EventHandler modeChangesHandler);
        public void StopUnit(EventHandler modeChangesHandler);
    }
    #endregion

    #region Consoles abstracts & interfaces
    public abstract class ConsoleBasic : UIElementBasic
    {
        public abstract void ShowConsole();
    }

    /*public class CModeChangesArgs : EventArgs
    {
        public int CMode { get; }
    }
    public class PuzzleModeArgs : CModeChangesArgs
    {
        public string playerRespond { get; }
    }*/

    public abstract class ConsoleModeController : MonoBehaviour
    {
        public event EventHandler ConsoleModeChanged;
        protected void RaiseModeChange(System.Object sender) => 
            this.ConsoleModeChanged?.Invoke(sender, EventArgs.Empty);

        public void SubToCMChangeEvent(EventHandler handler)
            => this.ConsoleModeChanged += handler;

        public abstract void InitMode();
        public abstract void ShowMode();
        public abstract void HideMode();
    }
    #endregion

    #region Consoles element classes
    public class UIElementBasic : MonoBehaviour
    {
        public bool isShow
        {
            get => this.gameObject.activeSelf;
            set => this.gameObject.SetActive(value);
        }
    }
    public class TextBox : UIElementBasic
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