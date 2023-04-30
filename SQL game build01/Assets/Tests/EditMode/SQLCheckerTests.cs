using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System;
using UnityEngine.TestTools;

public class SQLCheckerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestPlayerCorrectQuery()
    {
        string path = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        string anQuery = "SELECT * FROM Worm";
        string pQuery = "SELECT ID, Name, HP, Color FROM Worm";
        SQLChecker sqlCh = new SQLChecker(path);

        SQLResult expected = new SQLResult(false, true, sqlCh.GetQueryResult(anQuery));
        SQLResult actual = sqlCh.CheckAnswer(pQuery, anQuery);

        //var serializer = new JavaScriptSerializer();
        //var expectedString = serializer.Serialize(expected);
        //var actualString = serializer.Serialize(actual);

        Assert.AreEqual(expected.IsError, actual.IsError);
        Assert.AreEqual(expected.IsCorrect, actual.IsCorrect);
        Assert.AreEqual(expected.tableResult, actual.tableResult);
    }

    [Test]
    public void TestPlayerWrongQuery()
    {
        string path = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        SQLChecker sqlCh = new SQLChecker(path);
        string anQuery = "SELECT * FROM Worm";
        string pQuery = "SELECT * FROM Fruit";

        SQLResult expected = new SQLResult(false, false, sqlCh.GetQueryResult(pQuery));
        SQLResult actual = sqlCh.CheckAnswer(pQuery, anQuery);

        //var serializer = new JavaScriptSerializer();
        //var expectedString = serializer.Serialize(expected);
        //var actualString = serializer.Serialize(actual);

        Assert.AreEqual(expected.IsError, actual.IsError);
        Assert.AreEqual(expected.IsCorrect, actual.IsCorrect);
        Assert.AreEqual(expected.tableResult, actual.tableResult);
    }

    [Test]
    public void TestInvalidPlayerQuery()
    {
        string path = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        string anQuery = "SELECT * FROM Worm";
        string pQuery = "SELECT * FROM ";
        SQLChecker sqlCh = new SQLChecker(path);

        SQLResult expected = new SQLResult(true, false, null);
        SQLResult actual = sqlCh.CheckAnswer(pQuery, anQuery);

        try {
            sqlCh.GetQueryResult(pQuery);
        }
        catch (Exception e)
        {
            expected.tableResult = e.Message.ToString();
        }
        //var serializer = new JavaScriptSerializer();
        //var expectedString = serializer.Serialize(expected);
        //var actualString = serializer.Serialize(actual);

        Assert.AreEqual(expected.IsError, actual.IsError);
        Assert.AreEqual(expected.IsCorrect, actual.IsCorrect);
        Assert.AreEqual(expected.tableResult, actual.tableResult);
    }
}
