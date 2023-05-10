using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConsoleGenerals
{
    public class ConsolesMaster : MonoBehaviour
    {
        //[System.NonSerialized] private ConsoleMode _currentMode ;

        private PuzzleConsoleMaster _puzzleConsole;
        private DialogConsoleMaster _dialogConsole;

        //acquire SQL/Puzzle Master

        public void GetPuzzleResponse(string playerInput)
        {
            //SQL/Puzzle Master.GetResponse(playerResponse)
            Debug.Log("Player Input: " + playerInput);
        }

        private void puzzleConsoleInit()
        {
            _puzzleConsole = GameObject.Find("Puzzle Console").GetComponent<PuzzleConsoleMaster>();
            if (_puzzleConsole != null)
            {
                _puzzleConsole.ExcutionCalled += GetPuzzleResponse;
                Debug.Log("PuzzleConsole connected");
            }
        }

        #region UnityBasics

        private void Update()
        {
            if (_puzzleConsole == null) puzzleConsoleInit();
        }
        #endregion
    }
}

