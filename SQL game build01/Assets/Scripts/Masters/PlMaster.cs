using ChapNRoom;
using ConsoleGeneral;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlMaster : MonoBehaviour
{
    //Gameplay control
    private ConsolesMaster _consoleController;
    private ChaptersMaster _roomTraverseController;
    //Player control
    private PlInterection _interactionController;
    //Dynamic bool;


    /*public void ConsoleSelectionTest(int modeIndex) 
    {
        if(modeIndex >= 0 && modeIndex <= 2) _consoleController.ShowConsole((ConsoleMode)modeIndex);
        else Debug.Log("No console with index: " +  modeIndex);
    }*/
    #region Room travalling controlling
    private void RoomTraverseControllerInit()
    {
        _roomTraverseController = FindAnyObjectByType<ChaptersMaster>();
        if (_roomTraverseController != null)
        {
            Debug.Log("Player: connected to chapter master");
            _interactionController.RoomTraverseCalled += TravelToNeighborRoom;
        }
    }
    private void TravelToNeighborRoom(RoomDirection direction)
    {
        Debug.Log("Player: call for room travel");
        _roomTraverseController.GoToNeigborRoom(direction);
    }
    #endregion

    #region Interaction controlling
    private void InteractionConInit()
    {
        _interactionController = FindAnyObjectByType<PlInterection>();
        if (_interactionController != null)
        {
            Debug.Log("Player: Interaction controller connected");
            //(re)connect all comm;
            RoomTraverseControllerInit();
            _interactionController.InteractionCalled += PassPMToConsole;
        }
    }
    #endregion

    #region Console controlling
    private void ConsoleControllerInit()
    {
        _consoleController = FindAnyObjectByType<ConsolesMaster>();
        if (_consoleController != null) Debug.Log("Player: connected to console master");
    }
    private void PassPMToConsole(PuzzleMaster pm)
    {
        //Debug.Log("PlayerMaster: PM sended to Console");
        _consoleController.ShowConsole(pm);
    }
    #endregion

    #region Unity Basics
    private void Update()
    {
        //Dummy loading
        if(_consoleController == null) ConsoleControllerInit();
        if(_interactionController == null) InteractionConInit();
        //Dumbest line ... waiting for proper loading system.
        //if (_roomTraverseController == null && _interactionController != null) RoomTraverseControllerInit();

    }
    #endregion
}
