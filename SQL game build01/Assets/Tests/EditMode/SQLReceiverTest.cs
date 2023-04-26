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
        Assert.AreEqual(true, sqlRe.haveBannedWord("TABLE creAte Persons PersonID int, LastName varchar(255), FirstName varchar(255), Address varchar(255), City varchar(255)"));
    }

    [Test]
    public void UseBannedWord_UPDATE()
    {
        SQLReceiver sqlRe = new SQLReceiver();

        //Assert
        Assert.AreEqual(true, sqlRe.haveBannedWord("UPDATE Customers SET ContactName = 'Alfred Schmidt', City = 'Frankfurt' WHERE CustomerID = 1; "));
        Assert.AreEqual(true, sqlRe.haveBannedWord("SELECT * FROM Worms WHERE color = 'Red';UPDATE"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("upDate Customers SET ContactName = 'Alfred Schmidt', City = 'Frankfurt' WHERE CustomerID = 1; "));
        Assert.AreEqual(true, sqlRe.haveBannedWord("Customers UPDATE SET ContactName = 'Alfred Schmidt', City = 'Frankfurt' WHERE CustomerID = 1; "));
    }

    [Test]
    public void UseBannedWord_DELETE()
    {
        SQLReceiver sqlRe = new SQLReceiver();

        // Assert
        Assert.AreEqual(true, sqlRe.haveBannedWord("DELETE FROM Customers WHERE CustomerName='Alfreds Futterkiste';"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("SELECT * FROM Worms WHERE color = 'Red';DELETE"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("DELeTE FROM Customers WHERE CustomerName='Alfreds Futterkiste';"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("FROM DELETE Customers WHERE CustomerName='Alfreds Futterkiste';"));
    }

    [Test]
    public void UseBannedWord_INSERT()
    {
        SQLReceiver sqlRe = new SQLReceiver();

        // Assert
        Assert.AreEqual(true, sqlRe.haveBannedWord("INSERT INTO Customers (CustomerName, ContactName, Address, City, PostalCode, Country) VALUES('Cardinal', 'Tom B. Erichsen', 'Skagen 21', 'Stavanger', '4006', 'Norway'); "));
        Assert.AreEqual(true, sqlRe.haveBannedWord("SELECT * FROM Worms WHERE color = 'Red';INSERT"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("InSERT INTO Customers (CustomerName, ContactName, Address, City, PostalCode, Country) VALUES('Cardinal', 'Tom B. Erichsen', 'Skagen 21', 'Stavanger', '4006', 'Norway'); "));
        Assert.AreEqual(true, sqlRe.haveBannedWord("INTO INSERT Customers (CustomerName, ContactName, Address, City, PostalCode, Country) VALUES('Cardinal', 'Tom B. Erichsen', 'Skagen 21', 'Stavanger', '4006', 'Norway'); "));
    }

    [Test]
    public void UseBannedWord_DROP()
    {
        SQLReceiver sqlRe = new SQLReceiver();

        // Assert
        Assert.AreEqual(true, sqlRe.haveBannedWord("DROP DATABASE testDB;"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("SELECT * FROM Worms WHERE color = 'Red';DROP DATABASE testDB;"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("DRoP DATABASE testDB;"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("DATABASE DROP testDB;"));
    }

    [Test]
    public void UseBannedWord_ALTER()
    {
        SQLReceiver sqlRe = new SQLReceiver();

        // Assert
        Assert.AreEqual(true, sqlRe.haveBannedWord("ALTER TABLE Customers ADD Email varchar(255); "));
        Assert.AreEqual(true, sqlRe.haveBannedWord("SELECT * FROM Worms WHERE color = 'Red';ALTER;"));
        Assert.AreEqual(true, sqlRe.haveBannedWord("ALtER TABLE Customers ADD Email varchar(255); "));
        Assert.AreEqual(true, sqlRe.haveBannedWord("TABLE ALTER Customers ADD Email varchar(255); "));
    }
}
