using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SQLReceiver : MonoBehaviour
{
    private string[] bannedWord = {"create", "update", "delete", "insert", "drop", "alter", "truncate", "grant", "revoke", "commit", "rollback", "savepoint"};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool haveBannedWord(string sqlCommand)
    {
        string[] sqlWords = sqlCommand.ToLower().Split(' ');

        foreach(string word in sqlWords)
        {
            if (bannedWord.Contains(word))
            {
                Debug.Log("You don't have permission to use this command: \"Create\", \"Insert\", \"Update\", \"Delete\"");
                return true;
            }
        }

        return false;
    }
}
