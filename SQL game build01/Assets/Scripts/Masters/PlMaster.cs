using ConsoleGeneral;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlMaster : MonoBehaviour
{
    private ConsolesMaster _consoleController;

    public void ConsoleSelectionTest(int modeIndex) 
    {
        if(modeIndex >= 0 && modeIndex <= 2) _consoleController.ShowConsole((ConsoleMode)modeIndex);
        else Debug.Log("No console with index: " +  modeIndex);
    }

    private void ConsoleControllerInit()
    {
        _consoleController = FindAnyObjectByType<ConsolesMaster>();
        if (_consoleController != null) Debug.Log("Player: connected to console master");
    }

    #region Unity Basics

    private void Update()
    {
        if(_consoleController == null) ConsoleControllerInit();
    }

    #endregion
}
