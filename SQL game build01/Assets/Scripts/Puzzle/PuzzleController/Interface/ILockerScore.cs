using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Puzzle.PuzzleController.Interface
{
    public interface ILockerScore
    {

        // Must subscribe score manager because It can know when total score is at this threshold.
        int ScoreUse { get; }
        bool IsLocked { get; }

        //void Unlock();
    }
}
