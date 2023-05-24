
using TMPro;
using UnityEngine;

namespace ConsoleGeneral
{
    public delegate void ExcuteButtonHandler(string playerInput);

    public enum ConsoleMode { ExploreMode, PuzzleMode, DialogMode}

    public class ConsoleBasic : MonoBehaviour
    {
        public void ToHide(bool hide)
        {
            if (hide) this.gameObject.SetActive(false);
            else this.gameObject.SetActive(true);
        }
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