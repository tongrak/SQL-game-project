using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class PuzzleMasterTest
{
    [UnityTest]
    public IEnumerator LoadPuzzleTest()
    {
        var gameObject = new GameObject();
        // construct puzzleMaster
        var pm = gameObject.AddComponent<PuzzleMaster>();
        pm.ConstructForTest(PuzzleType.query, "{\"dialog\":[\"ตรงนี้มีหนอนที่ท่าทางอันตรายอยู่\", \"โดยหนอนเหล่านั้นจะมีสีเหลือง\"],\"question\":\"แสดงข้อมูลทั้งหมดของ Worm ที่มี Color เป็น Yellow\",\"answer\":\"SELECT * FROM Worm WHERE Color = 'Yellow'\",\"condition\":[\"condition1\",\"condition2\"]}", DatabaseFile.ChapterDemo);

        // Expected value
        string[] expected_dialog = { "ตรงนี้มีหนอนที่ท่าทางอันตรายอยู่", "โดยหนอนเหล่านั้นจะมีสีเหลือง" };
        string expected_question = "แสดงข้อมูลทั้งหมดของ Worm ที่มี Color เป็น Yellow";
        string expected_answer = "SELECT * FROM Worm WHERE Color = 'Yellow'";
        string[] expected_condition = { "condition1", "condition2" };

        // Assert
        yield return null;
        Assert.AreEqual(expected_dialog, pm.Dialog);
        Assert.AreEqual(expected_question, pm.Question);
        Assert.AreEqual(expected_answer, pm.AnswerQuery);
        Assert.AreEqual(expected_condition, pm.Condition);
        
    }
}
