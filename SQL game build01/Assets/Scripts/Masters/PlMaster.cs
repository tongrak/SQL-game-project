using ChapNRoom;
using ConsoleGeneral;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterGeneral
{
    public class PlMaster : MonoBehaviour
    {
        [Header("Spawning configuration")]
        [SerializeField] private GameObject _playerPrefab;
        //[SerializeField] private string _playerHolderObjectName = "Player holder";

        //Gameplay control
        private ConsolesMaster _consoleController;
        private ChaptersMaster _roomController;
        //Player control
        private PlInterection _interactionController;
        //Dynamic Var
        private GameObject _currPlayerObj;


        #region Listener Functions
        //to Consoles 
        private void PassPMToConsole(PuzzleMaster pm)
        {
            _consoleController.ShowConsole(pm);
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
        private void GameplayInit(int maxLoadCount)
        {
            bool loadComplete = false;
            int loadCount = 0;
            string errorMessage = string.Empty;


            while (!loadComplete && loadCount < maxLoadCount)
            {
                try
                {
                    _consoleController = MasterHelper.GetMasterWithType<ConsolesMaster>();
                    _roomController = MasterHelper.GetMasterWithType<ChaptersMaster>();
                }
                catch (System.Exception ex)
                {
                    loadCount++;
                    errorMessage = ex.Message;
                }
                loadComplete = true;
            }

            if (!loadComplete) throw new MissingComponentException("Fail to initiate PlayerMaster gameplay component due to: " + errorMessage); //do raise fail to init to MastersController
            else
            {
                _roomController.RoomLoaded += SpawnPlayer;
            }
        }

        private void PlayerControlInit()
        {
            try
            {
                _interactionController = MasterHelper.GetMasterWithType<PlInterection>();
            }
            catch (System.Exception ex) { throw new MissingFieldException("Fail to get player's component due to: " + ex.Message); }

            InteractionControllerListenerInit();
        }

        private void InteractionControllerListenerInit()
        {
            _interactionController.InteractionCalled += PassPMToConsole;
            _interactionController.RoomTraverseCalled += TravelToNeighborRoom;
        }

        #endregion

        #region Spawning Functions
        private void SpawnPlayer(RoomSpawningDetail rsd)
        {
            _currPlayerObj = GameObject.Instantiate(_playerPrefab, rsd.spawn.transform.position, rsd.spawn.transform.rotation, rsd.playerholder.transform);
            _currPlayerObj.SetActive(false);
            PlayerControlInit();
            _currPlayerObj.SetActive(true);
        }
        private void DespawnPlayer()
        {
            GameObject.Destroy(_currPlayerObj);
        }
        #endregion

        #region Unity Basics
        private void Start()
        {
            //try to initiate 5 time
            GameplayInit(5);
        }
        private void Update()
        {

        }
        #endregion
    }
}


