using ChapNRoom;
using Codice.Client.BaseCommands;
using ConsoleGeneral;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterGeneral
{
    public class PlayerScriptManager : MonoBehaviour
    {
        [Header("Spawning configuration")]
        [SerializeField] private GameObject _playerPrefab;
        //Gameplay control
        private ConsolesManager _consoleController;
        private ChaptersManager _roomController;
        //Player control
        private PlInterection _interactionController;
        //Dynamic Var
        private GameObject _currPlayerObj;


        #region Listener Functions
        //to Consoles 
        private void PassPMToConsole(PuzzleMaster pm)
        {
            _consoleController.ShowConsoleFor(pm);
        }
        //to Chapter
        private void TravelToNeighborRoom(RoomDirection direction)
        {
            Debug.Log("Player: call for room travel");
            _interactionController.InteractionCalled -= PassPMToConsole;
            _interactionController.RoomTraverseCalled -= TravelToNeighborRoom; //remove old travelling listening function
            _roomController.GoToNeigborRoom(direction);
            DespawnPlayer();
        }
        #endregion

        #region Init Functions
        private void PlayerControlInit()
        {
            try
            {
                _interactionController = MasterHelper.GetObjectWithType<PlInterection>();
            }
            catch (System.Exception ex) { throw new MissingFieldException(string.Format("Fail to get player's component due to: {0}", ex.Message)); }

            InteractionListenerInit();
        }

        private void InteractionListenerInit()
        {
            _interactionController.InteractionCalled += PassPMToConsole;
            _interactionController.RoomTraverseCalled += TravelToNeighborRoom;
        }
        #endregion

        #region Spawning Functions
        private void SpawnPlayer(RoomSpawningDetail rsd)
        {
            _currPlayerObj = GameObject.Instantiate(_playerPrefab, rsd.spawn.transform.position, rsd.spawn.transform.rotation, rsd.playerholder.transform);
            PlayerControlInit();
        }
        private void DespawnPlayer()
        {
            GameObject.Destroy(_currPlayerObj);
        }
        #endregion

        #region Unity Basics
        private void Start()
        {
            bool initComplete = false; 
            try
            {
                _consoleController = MasterHelper.GetObjectWithType<ConsolesManager>();
                _roomController = MasterHelper.GetObjectWithType<ChaptersManager>();
                initComplete = true;
            }
            catch (FailToGetUnityObjectException fgo)
            {
                Debug.LogException(fgo);
                //Handling above object initiate failure
                //Somehow....
                throw new System.Exception(fgo.Message);
            }
            Debug.Log("PlMaster: init complete");
            if (initComplete) _roomController.RoomLoaded += SpawnPlayer;
        }
        #endregion
    }
}