using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQLValidator
{
    private static SQLValidator instance = new SQLValidator();

    private SQLValidator()
    {

    }

    public static SQLValidator GetInstance()
    {
        return instance;
    }

    public bool isQueryInvalid(string query)
    {
        return true;
    }
}
