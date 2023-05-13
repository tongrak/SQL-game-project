using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace ConsoleGeneral
{
    public class QuestBarMaster : ConsoleBasic
    {
        [Header("Displaying element")]
        [SerializeField] private TextMeshProUGUI _displayElement;

        private string _displayingString = "//QuestBar// /n//QuestBar//";

        public void ShowQuestBar(string quest)
        {
            _displayingString = quest;
            _displayElement.text = _displayingString;
            ToHide(false);
        }

        #region Unity Basics
        /*void Start()
        {
            ToHide(true);
        }*/
        #endregion
    }
}

