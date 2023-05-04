using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using UnityEngine.TestTools;

public class SQLValidatorTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void WrongDBPath_Test()
    {
        string _dbPath = "URI=file:" + Application.dataPath + "/DatabaseA/DemoDatabase.db";
        string _query = "";

        Assert.That(() => SQLValidator.GetInstance().validatePathAndQuery(_dbPath, _query), Throws.TypeOf<SqliteException>());
    }

    [Test]
    public void HaveBanndedQuery_Test()
    {
        string _dbPath = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        string _query = "Insert";

        Assert.That(() => SQLValidator.GetInstance().validatePathAndQuery(_dbPath, _query), Throws.TypeOf<SqliteException>());

    }

    [Test]
    public void InvalidQuery_Test()
    {
        string _dbPath = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        string _query = "select * from";

        Assert.That(() => SQLValidator.GetInstance().validatePathAndQuery(_dbPath, _query), Throws.TypeOf<SqliteException>());
    }

    [Test]
    public void ValidQuery_Test()
    {
        string _dbPath = "URI=file:" + Application.dataPath + "/Database/DemoDatabase.db";
        string _query = "select * from Worm";

        Assert.AreEqual(true, SQLValidator.GetInstance().validatePathAndQuery_forTest(_dbPath, _query));
    }
}
