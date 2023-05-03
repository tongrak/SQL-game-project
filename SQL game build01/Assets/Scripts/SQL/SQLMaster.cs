using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface SQLMasterInt
{
    void GetResult(string pQuery, string anQuery);
}

public class SQLMaster : MonoBehaviour, SQLMasterInt
{
    //[SerializeField] PuzzleMaster puzzleMas;

    private SQLChecker checker;
    private SQLReceiver receiver;
    private string _dbPath;
    //private string _dbPath = puzzleMas.Get_dbPath;

    // Start is called before the first frame update
    void Start()
    {
        if(receiver == null)
        {
            receiver = new SQLReceiver();
        }
        if(checker == null)
        {
            checker = new SQLChecker(_dbPath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetResult(string pQuery, string anQuery)
    {
        if (receiver.haveBannedWord(pQuery))
        {
            Debug.Log("Player query is not valid.");
        }
        else
        {
            SQLResult result;
            result = checker.CheckAnswer(pQuery, anQuery);
        }
    }
}
