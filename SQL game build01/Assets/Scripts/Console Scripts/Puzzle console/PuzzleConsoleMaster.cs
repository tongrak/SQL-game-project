using ConsoleGenerals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConsoleMaster : ConsoleBasic
{
    [SerializeField] public string currOutputString;
    [SerializeField] private bool _calledForResponse = false;

    private string _currInputString;
    //private PuzzleMaster _currPuzzleMaster;

    //public void InnitPuzzleMaster(PuzzleMaster pm)

    #region Input Box
    public void UpdateCurrInput(string s)
    {
        _currInputString = s;
    }

    #endregion

    public string UpdateOutputString()
    {
        currOutputString = _currInputString;
        return currOutputString;
    }   

    #region Excute Butt
    public void ExcutionButtonAct()
    {
        Debug.Log("Button Click");
        _calledForResponse = true;
    }

    public bool GetCalled()
    {
        if (_calledForResponse)
        {
            _calledForResponse = false;
            return true;
        }
        return false;
    }
    #endregion
}
