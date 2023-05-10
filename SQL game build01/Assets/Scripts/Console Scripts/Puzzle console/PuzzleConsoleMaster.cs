
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGenerals
{
    public class PuzzleConsoleMaster : ConsoleBasic
    {
        public event ExcuteButtonHandler ExcutionCalled;

        private string _currOutputString;
        private string _currInputString;

        #region Input Box
        public void UpdateCurrInput(string s)
        {
            _currInputString = s;
        }
        #endregion

        #region Excute Butt
        public virtual void ExcutionButtonAct()
        {
            _currOutputString = _currInputString;
            ExcutionCalled?.Invoke(_currOutputString);
        }
        #endregion
    }
}

