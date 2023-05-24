using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConsoleGeneral
{
    public class ConfirmButton : TextBox
    {
        [SerializeField] private Button _buttonElement;
        public bool buttonEnable
        {
            get
            {
                return _buttonElement.enabled;
            }
            set
            {
                _buttonElement.enabled = value;
            }
        }

    }
}

