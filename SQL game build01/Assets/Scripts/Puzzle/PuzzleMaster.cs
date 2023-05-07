using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMaster : MonoBehaviour
{
    enum PuzzleType { query, keyItem, queryAndKeyItem, fillQueryCommand, tellQueryResult}
    [SerializeField] PuzzleType puzzleType;

    [SerializeField] TextAsset textfile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonMethod()
    {
        switch (puzzleType)
        {
            case PuzzleType.query:
                Debug.Log("This is query puzzle.");
                break;
            case PuzzleType.keyItem:
                Debug.Log("This is key item puzzle.");
                break;
            case PuzzleType.queryAndKeyItem:
                Debug.Log("This is query and key item puzzle.");
                break;
            case PuzzleType.fillQueryCommand:
                Debug.Log("This is fill query command puzzle.");
                break;
            default:
                Debug.Log("This is tell query result puzzle.");
                break;
        }
    }
}
