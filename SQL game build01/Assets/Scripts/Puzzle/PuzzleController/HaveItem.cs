using Puzzle;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Puzzle.PuzzleController.Interface
{
    public class HaveItem : MonoBehaviour, IHaveItem
    {
        [SerializeField] private KeyItem keyItemCarried;

        public KeyItem GetKeyItem()
        {
            return keyItemCarried;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}