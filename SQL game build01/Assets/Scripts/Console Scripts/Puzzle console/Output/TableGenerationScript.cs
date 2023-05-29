
using ConsoleGeneral;
using UnityEngine;

namespace PuzzleConsole
{
    public class TableGenerationScript : UIElementBasic, TableElement
    {
        [SerializeField] private GameObject _columnPrefab;


        public void SetDisplayData(string[][] inData)
        {
            foreach (Transform col in this.transform)
            {
                Destroy(col.gameObject);
            }

            foreach (var col in inData)
            {
                GameObject colObjRef = Instantiate(_columnPrefab, this.transform);
                ColumnElement colEleRef = colObjRef.GetComponent<ColumnEleScript>();
                if (colEleRef != null) colEleRef.SetDisplayCells(col);
                else Debug.LogWarning("No column script detected");
            }
        }

        /*private void DeleteOldCol()
        {
            Object[] colObj = FindObjectsOfType(typeof(ColumnEleScript));
            foreach (var col in colObj) Destroy(col);
        }*/

        #region Unity Basic
        private void Start()
        {
            /*Hide();*/
        }
        #endregion
    }
}
    

