using Puzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Puzzle.PuzzleController.Interface
{
    public interface IQueryPuzzle
    {
        string[] Dialog { get; }
        string Question { get; }
        string[] ConditionMessage { get; }
        int CurrScore { get; }
        int ExecutedNum { get; }
        PuzzleResult BestPuzzleResult { get; }

        PuzzleResult AnswerPuzzle(string playerQuery);
        void ResetExecutedNum();
    }
}
