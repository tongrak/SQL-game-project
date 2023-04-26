using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SQLReceiverTest
{
    [Test]
    public void NotUseBannedWord()
    {
        // Assign
        SQLReceiver sqlRe = new SQLReceiver();

        //Assert
        Assert.AreEqual(false, sqlRe.haveBannedWord("SELECT * FROM Worms WHERE color = 'Red'"));
    }

    [Test]
    public void UseBannedWord_CREATE()
    {
        // Assign
        SQLReceiver sqlRe = new SQLReceiver();

        //Assert
        Assert.AreEqual(true, sqlRe.haveBannedWord("SELECT * FROM Worms WHERE color = 'Red';Create"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("CREATE TABLE Persons PersonID int, LastName varchar(255), FirstName varchar(255), Address varchar(255), City varchar(255)"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("create TABLE Persons PersonID int, LastName varchar(255), FirstName varchar(255), Address varchar(255), City varchar(255)"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("TABLE CREATE Persons PersonID int, LastName varchar(255), FirstName varchar(255), Address varchar(255), City varchar(255)"));

    }
}
