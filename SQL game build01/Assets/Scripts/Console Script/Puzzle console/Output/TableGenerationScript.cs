
using UnityEngine;

namespace PuzzleConsole
{
    public class TableGenerationScript : OutputTableElement, TableElement
    {
        [SerializeField] private GameObject _columnPrefab;

        public void SetDisplayData(string[][] inData)
        {
            foreach (var col in inData)
            {
                GameObject colObjRef = Instantiate(_columnPrefab, this.transform);
                ColumnElement colEleRef = colObjRef.GetComponent<ColumnEleScript>();
                if (colEleRef != null) colEleRef.SetDisplayCells(col);
                else Debug.LogWarning("No column script detected");
            }
        }

        #region Unity Basic
        private void Start()
        {
            /*Hide();*/
        }
        #endregion
    }
}
    

