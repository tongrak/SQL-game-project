
using UnityEngine;

namespace PuzzleConsole
{
    class ColumnEleScript : OutputTableElement, ColumnElement
    {
        [SerializeField] private GameObject _cellElePreFabs;

        public void SetDisplayCells(string[] inTexts)
        {
            foreach (var text in inTexts)
            {
                GameObject cellRef = Instantiate(_cellElePreFabs, this.transform);
                CellElement cellEleRef = cellRef.GetComponent<CellEleScript>();
                if (cellEleRef != null) cellEleRef.SetDisplayText(text);
                else Debug.LogWarning("Cann't connect to cell script");
            }
            this.Show();
        }

        #region Unity Basic
        private void Start()
        {
            
        }
        #endregion
    }

}

