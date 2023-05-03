using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleConsoleScript : MonoBehaviour
{

    private string _CurrInputString;

    #region Input Box
    public void UpdateCurrInput(string s)
    {
        _CurrInputString = s;
    }

    #endregion

    #region Execution Button
    public void ExecuteInput()
    {
        Debug.Log(_CurrInputString);
    }
    #endregion
}
