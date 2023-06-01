using PuzzleConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConsoleGeneral
{
    public class PuzzleConsoleController : ConsoleBasic
    {
        //For raising execution call
        public event ExcuteButtonHandler ExcutionCalled;

        [Header("Displaying Element")]
        [SerializeField] private TableGenerationScript _tableGenerator;
        [SerializeField] private Button _buttonElement;
        //Input box feilds;
        private string _currOutputString;
        private string _currInputString;
        //Config fields
        private bool _canExecute = true;
        [Header("Configure variable")]
        [SerializeField] private int _exeBuffer = 1;

        public override void ShowConsole()
        {
            this.isShow = true;
        }

        public void DisplayOutput(string[][] data)
        {
            _tableGenerator.SetDisplayData(data);
            _tableGenerator.isShow = true;
        }

        #region Input Box
        public void UpdateCurrInput(string s)
        {
            _currInputString = s;
        }
        #endregion

        #region Excute Butt
        IEnumerator ExecutionBuffer(int sec)
        {
            yield return new WaitForSeconds(sec);
            _canExecute = _buttonElement.interactable = true;
        }
        public virtual void ExcutionButtonAct()
        {
            _currOutputString = _currInputString;
            if (_canExecute)
            {
                _canExecute = _buttonElement.interactable = false;
                ExcutionCalled?.Invoke(_currOutputString);
                StartCoroutine(ExecutionBuffer(_exeBuffer));
            }
        }
        #endregion
    }
}

