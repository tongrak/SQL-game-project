
using TMPro;
using UnityEngine;

namespace ConsoleGeneral
{
    public delegate void ExcuteButtonHandler(string playerInput);

    public enum ConsoleMode { ExploreMode, PuzzleMode, DialogMode}

    public static class CMStarterFactory
    {
        public static ConModeStarterUnit CreateDialogMode(DialogConsoleMaster dialogConsole, string[] dialogs, string confirmMessage)
                => new DialogConsoleStarter(dialogConsole, dialogs, confirmMessage);

        public static ConModeStarterUnit CreatePuzzleMode(PuzzleConsoleMaster puzzleConsole)
               => new PuzzleConsoleStarter(puzzleConsole);
    }

    public interface ConModeStarterUnit
    {
        public void StartConsole();
    }

    public abstract class ConsoleBasic : MonoBehaviour
    {
        public bool isShow
        {
            get => this.gameObject.activeSelf;
            set => this.gameObject.SetActive(value);
        }
        public abstract void ShowConsole();
    }

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
}