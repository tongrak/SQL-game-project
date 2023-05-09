using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGenerals
{
    public class ConsolesMaster : MonoBehaviour
    {
        //[System.NonSerialized] private ConsoleMode _currentMode ;

        private PuzzleConsoleMaster _puzzleConsole;

        

        //acquire SQL/Puzzle Master

        public void GetPuzzleResponse()
        {
            string playerResponse = _puzzleConsole.UpdateOutputString();

            //SQL/Puzzle Master.GetResponse(playerResponse)
            Debug.Log("Player Input: " + playerResponse);
        }

        #region UnityBasics
        private void Awake()
        {
            //init console mode to Explore
            //_currentMode = ConsoleMode.ExploreMode;

            //init child console
            //
            //init explore console
            //init dialog console
        }

        private void Update()
        {
            if ( _puzzleConsole == null )
            {
                _puzzleConsole = GameObject.Find("Puzzle Console").GetComponent<PuzzleConsoleMaster>();
            }
            else
            {
                if (_puzzleConsole.GetCalled()) { GetPuzzleResponse(); }
            }
        }
        #endregion
    }
}

