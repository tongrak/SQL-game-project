using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SQLCheckerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void SQLCheckerTestsSimplePasses()
    {
        string path = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        string query = "SELECT * FROM Worm";
        SQLChecker sqlCh = new SQLChecker(path);

        Assert.AreEqual("", sqlCh.GetQueryResult(query));
    }
}
