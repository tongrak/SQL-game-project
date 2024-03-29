using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Elements.Puzzle
{
    public class CellEleScript : UIElementBasic, CellElement
    {
        [Header("Cell's Object Elements")]
        [SerializeField] private TextMeshProUGUI _textElement;
        [SerializeField] private Image _BgImage;

        int _fontSize { get; set; } = 22;

        #region Configuration Controll
        public void SetDisplayText(string inText)
        {
            _textElement.text = inText;
            this.isShow = true;
        }
        #endregion

        #region Unity Basic
        private void Start()
        {
            _textElement.fontSize = _fontSize;
        }
        #endregion
    }

}

