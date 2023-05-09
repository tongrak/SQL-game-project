
using UnityEngine;

namespace ConsoleGenerals
{
    enum ConsoleMode { ExploreMode, PuzzleMode}

    public class ConsoleBasic : MonoBehaviour
    {
        public void ToHide(bool hide)
        {
            if (hide) this.gameObject.SetActive(false);
            else this.gameObject.SetActive(true);
        }
    }
}