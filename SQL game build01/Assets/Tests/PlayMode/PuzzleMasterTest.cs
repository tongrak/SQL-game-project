using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using PuzzleController;
using System;

public class PuzzleMasterTest
{
    [UnityTest]
    public IEnumerator LoadPuzzleTest()
    {
        var gameObject = new GameObject();
        // construct puzzleMaster
        var pm = gameObject.AddComponent<QueryPuzzleMaster>();
        pm.ConstructForTest(PuzzleType.QueryPuzzle, "{\"dialog\":[\"ตรงนี้มีหนอนที่ท่าทางอันตรายอยู่\", \"โดยหนอนเหล่านั้นจะมีสีเหลือง\"],\"question\":\"แสดงข้อมูลทั้งหมดของ Worm ที่มี Color เป็น Yellow\",\"answer\":\"SELECT * FROM Worm WHERE Color = 'Yellow'\",\"condition\":[\"condition1\",\"condition2\"]}", DatabaseFile.ChapterDemo);
        yield return null;

        // Expected value
        string[] expected_dialog = { "ตรงนี้มีหนอนที่ท่าทางอันตรายอยู่", "โดยหนอนเหล่านั้นจะมีสีเหลือง" };
        string expected_question = "แสดงข้อมูลทั้งหมดของ Worm ที่มี Color เป็น Yellow";
        string expected_answer = "SELECT * FROM Worm WHERE Color = 'Yellow'";
        string[] expected_condition = { "condition1", "condition2" };

        // Assert
        Assert.AreEqual(expected_dialog, pm.Dialog);
        Assert.AreEqual(expected_question, pm.Question);
        Assert.AreEqual(expected_answer, pm.AnswerQuery);
        Assert.AreEqual(expected_condition, pm.Condition);
    }

    [UnityTest]
    public IEnumerator CorrectQuery()
    {
        yield return null;
        //var gameObject = new GameObject();
        //// construct puzzleMaster
        //var pm = gameObject.AddComponent<PuzzleMaster>();
        //pm.ConstructForTest(PuzzleType.query, "{\"dialog\":[\"ตรงนี้มีหนอนที่ท่าทางอันตรายอยู่\", \"โดยหนอนเหล่านั้นจะมีสีเหลือง\"],\"question\":\"แสดงข้อมูลทั้งหมดของ Worm ที่มี Color เป็น Yellow\",\"answer\":\"SELECT * FROM Worm WHERE Color = 'Yellow'\",\"condition\":[\"condition1\",\"condition2\"]}", DatabaseFile.ChapterDemo);
        //yield return null;

        //QueryResultDeliver resultDeliver = QueryResultDeliver.GetInstance();
        //string playerQuery = "SELECT id, name, hp, color FROM Worm WHERE Color = 'Yellow'";

        //// Expected
        //string expected_result = resultDeliver.GetQueryResult(pm.DBPath, pm.AnswerQuery);
        //expected_result = "{queryResult:" + expected_result + ",score:1}";

        //// Actual
        //string actual_result = pm.GetResult(playerQuery);

        //// Assert
        //Assert.AreEqual(expected_result, actual_result);
    }

    [UnityTest]
    public IEnumerator WrongQuery()
    {
        yield return null;
        //var gameObject = new GameObject();
        //// construct puzzleMaster
        //var pm = gameObject.AddComponent<PuzzleMaster>();
        //pm.ConstructForTest(PuzzleType.query, "{\"dialog\":[\"ตรงนี้มีหนอนที่ท่าทางอันตรายอยู่\", \"โดยหนอนเหล่านั้นจะมีสีเหลือง\"],\"question\":\"แสดงข้อมูลทั้งหมดของ Worm ที่มี Color เป็น Yellow\",\"answer\":\"SELECT * FROM Worm WHERE Color = 'Yellow'\",\"condition\":[\"condition1\",\"condition2\"]}", DatabaseFile.ChapterDemo);
        //yield return null;

        //QueryResultDeliver resultDeliver = QueryResultDeliver.GetInstance();
        //string playerQuery = "SELECT id, name, hp FROM Worm WHERE Color = 'Yellow'";

        //// Expected
        //string expected_result = resultDeliver.GetQueryResult(pm.DBPath, playerQuery);
        //expected_result = "{queryResult:" + expected_result + ",score:0}";

        //// Actual
        //string actual_result = pm.GetResult(playerQuery);

        //// Assert
        //Assert.AreEqual(expected_result, actual_result);
    }

    [UnityTest]
    public IEnumerator ErrorQuery()
    {
        yield return null;
        //var gameObject = new GameObject();
        //// construct puzzleMaster
        //var pm = gameObject.AddComponent<PuzzleMaster>();
        //pm.ConstructForTest(PuzzleType.query, "{\"dialog\":[\"ตรงนี้มีหนอนที่ท่าทางอันตรายอยู่\", \"โดยหนอนเหล่านั้นจะมีสีเหลือง\"],\"question\":\"แสดงข้อมูลทั้งหมดของ Worm ที่มี Color เป็น Yellow\",\"answer\":\"SELECT * FROM Worm WHERE Color = 'Yellow'\",\"condition\":[\"condition1\",\"condition2\"]}", DatabaseFile.ChapterDemo);
        //yield return null;

        //QueryResultDeliver resultDeliver = QueryResultDeliver.GetInstance();
        //string playerQuery = "SELECT id";

        //// Expected
        //string expected_result = "";
        //try
        //{
        //    SQLValidator.GetInstance().validatePathAndQuery(pm.DBPath, playerQuery);
        //}
        //catch(Exception e)
        //{
        //    expected_result = "{error:\"" + e.Message.ToString() + "\",score:0}";
        //}

        ////Actual
        //string actual_result = pm.GetResult(playerQuery);

        //// Assert
        //Assert.AreEqual(expected_result, actual_result);
    }
}
