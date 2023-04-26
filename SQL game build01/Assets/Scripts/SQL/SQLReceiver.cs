using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SQLReceiver
{
    private string[] bannedWord = {"create", "update", "delete", "insert", "drop", "alter", "truncate", "grant", "revoke", "commit", "rollback", "savepoint"};

    public SQLReceiver()
    {

    }

    public bool haveBannedWord(string sqlCommand)
    {
        string[] sqlWords = sqlCommand.ToLower().Split(' ', ';');

        foreach(string word in sqlWords)
        {
            if (bannedWord.Contains(word))
            {
                string warningWord = "You don't have permission to use this command:";

                for (int i = 0; i < bannedWord.Length; i++)
                {
                    warningWord += " \"" + bannedWord[i] + "\"";
                    if(i < bannedWord.Length - 1)
                    {
                        warningWord += ",";
                    }
                }

                Debug.Log(warningWord);
                return true;
            }
        }

        return false;
    }
}
