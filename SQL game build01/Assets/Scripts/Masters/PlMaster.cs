using ConsoleGeneral;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlMaster : MonoBehaviour
{
    private PlInterection _interactionController;

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

    private void InteractionConInit()
    {
        _interactionController = FindAnyObjectByType<PlInterection>();
        if (_interactionController != null)
        {
            Debug.Log("Player: Interaction controller connected");
            _interactionController.InteractionCalled += PassPMToConsole;
        }
    }

    private void PassPMToConsole(PuzzleMaster pm)
    {
        Debug.Log("PlayerMaster: PM sended to Console");
        _consoleController.ShowConsole(pm);
    }

    #region Unity Basics

    private void Update()
    {
        //Dummy loading
        if(_consoleController == null) ConsoleControllerInit();
        if(_interactionController == null) InteractionConInit();
    }

    #endregion
}
