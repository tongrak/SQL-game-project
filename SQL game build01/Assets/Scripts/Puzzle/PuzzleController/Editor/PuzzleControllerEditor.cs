using Assets.Scripts.Puzzle.PuzzleController.Interface;
using Puzzle;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController
{
    [CustomEditor(typeof(PuzzleController))]
    public class PuzzleControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PuzzleController puzzleController = (PuzzleController)target;
        }
    }
}