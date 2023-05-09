
using UnityEngine;

namespace PuzzleConsole
{
    public class OutputTableElement : MonoBehaviour
    {
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }

    interface CellElement
    {
        void SetDisplayText(string inText);
    }

    interface ColumnElement
    {
        void SetDisplayCells(string[] inTexts);
    }

    interface TableElement
    {
        void SetDisplayData(string[][] inData);
    }
}
