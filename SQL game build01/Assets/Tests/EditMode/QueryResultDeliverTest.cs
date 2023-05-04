using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class QueryResultDeliverTest
{
    // A Test behaves as an ordinary method

    [Test]
    public void DiffQuery_SameResult_Test()
    {
        string _dbPath = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        string expected_query = "Select * from Worm";
        string actual_query_1 = "SELECT ID, Name, HP, Color FROM Worm";
        string actual_query_2 = "SELECT w.ID, w.Name, w.HP, w.Color FROM Worm w";

        Assert.AreEqual(QueryResultDeliver.GetInstance().GetQueryResult(_dbPath, expected_query), QueryResultDeliver.GetInstance().GetQueryResult(_dbPath, actual_query_1));
        Assert.AreEqual(QueryResultDeliver.GetInstance().GetQueryResult(_dbPath, expected_query), QueryResultDeliver.GetInstance().GetQueryResult(_dbPath, actual_query_2));
    }

    [Test]
    public void DiffQuery_DiffResult_Test()
    {
        string _dbPath = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        string expected_query = "Select * from Worm";
        string actual_query = "SELECT ID, Name, HP FROM Worm";

        Assert.AreNotEqual(QueryResultDeliver.GetInstance().GetQueryResult(_dbPath, expected_query), QueryResultDeliver.GetInstance().GetQueryResult(_dbPath, actual_query));
    }
}
