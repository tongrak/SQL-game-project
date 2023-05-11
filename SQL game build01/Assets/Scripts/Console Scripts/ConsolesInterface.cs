
using UnityEngine;

namespace ConsoleGenerals
{
    public delegate void ExcuteButtonHandler(string playerInput);

    public enum ConsoleMode { ExploreMode, PuzzleMode, DialogMode}

    public class ConsoleBasic : MonoBehaviour
    {
        public void ToHide(bool hide)
        {
            if (hide) this.gameObject.SetActive(false);
            else this.gameObject.SetActive(true);
        }
    }
}