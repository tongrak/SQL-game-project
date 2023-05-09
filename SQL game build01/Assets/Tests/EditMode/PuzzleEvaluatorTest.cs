using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PuzzleEvaluatorTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void DifferentQuery_SameResult()
    {
        //PuzzleEvaluator evaluator = PuzzleEvaluator.GetInstance();
        //QueryResultDeliver resultDeliver = QueryResultDeliver.GetInstance();

        //string dbPath = DatabaseFilePath.LocateDBPath(DatabaseFile.ChapterDemo);
        //string answerQuery = "SELECT * FROM Worm WHERE Color = 'Red'";
        //string playerQuery = "SELECT id, name, hp, color FROM Worm WHERE Color = 'Red'";

        //// Expected
        //string expected_result = resultDeliver.GetQueryResult(dbPath, answerQuery);
        //expected_result = "{queryResult:" + expected_result + ",score:1}";

        //// Actual
        //string actual_result = evaluator.EvalutateQuery(dbPath, answerQuery, playerQuery);

        //// Assert
        //Assert.AreEqual(expected_result, actual_result);

    }

    [Test]
    public void DifferentQuery_DiffResult()
    {
        //PuzzleEvaluator evaluator = PuzzleEvaluator.GetInstance();
        //QueryResultDeliver resultDeliver = QueryResultDeliver.GetInstance();

        //string dbPath = DatabaseFilePath.LocateDBPath(DatabaseFile.ChapterDemo);
        //string answerQuery = "SELECT * FROM Worm WHERE Color = 'Red'";
        //string playerQuery = "SELECT id, name, hp FROM Worm WHERE Color = 'Red'";

        //// Expected
        //string expected_result = resultDeliver.GetQueryResult(dbPath, playerQuery);
        //expected_result = "{queryResult:" + expected_result + ",score:0}";

        //// Actual
        //string actual_result = evaluator.EvalutateQuery(dbPath, answerQuery, playerQuery);

        //// Assert
        //Assert.AreEqual(expected_result, actual_result);
    }
}
