using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PuzzleConsole
{
    public class CellEleScript : MonoBehaviour, CellElement
    {
        [Header("TextElement")]
        [SerializeField] private TextMeshProUGUI _textElement;
        [SerializeField] private Image _BgImage;

        int _fontSize { get; set; }

        #region Visibility Control
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }
        #endregion

        #region Configuration Controll
        public void SetDisplayText(string inText)
        {
            _textElement.text = inText;
        }
        public void SetFontSize(int fontSize)
        {
            _fontSize = fontSize;
            /*_textElement.fontSize = _fontSize;*/
        }
        public
        #endregion

        #region Uniy basics
        void Start()
        {
            _textElement.text = "//Text//";
            _textElement.fontSize = _fontSize = 22;
        }
        #endregion
    }

}

