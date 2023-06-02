using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Gameplay.UI.Elements
{
    public class QuestBarController : ConsoleBasic
    {
        [Header("Displaying element")]
        [SerializeField] private TextMeshProUGUI _displayElement;

        private string _displayingString = "//QuestBar// /n//QuestBar//";

        public override void ShowConsole()
        {
            this.isShow = true;
        }

        public void ShowConsole(string quest)
        {
            _displayingString = quest;
            _displayElement.text = _displayingString;
            ShowConsole();
        }
    }
}

