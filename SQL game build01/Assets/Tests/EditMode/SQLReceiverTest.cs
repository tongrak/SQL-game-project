using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SQLReceiverTest
{
    [Test]
    public void TestQueryCommand()
    {
        // Assign
        SQLReceiver sqlRE = new SQLReceiver();

        //Assert
        Assert.AreEqual(false, sqlRE.haveBannedWord("SELECT * FROM Worms WHERE color = 'Red'"));
    }
}
