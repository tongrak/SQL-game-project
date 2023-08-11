using UnityEngine;

namespace Gameplay.UI.Elements.Puzzle
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
    }
}
    

