using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ConsoleGeneral
{
    public class ExploreModeStarter : CMStarterUnit
    {   
        public ConsoleMode mode => throw new System.NotImplementedException();

        public void StartUnit()
        {
            throw new System.NotImplementedException();
        }

        public void StartUnit(EventHandler modeChangesHandler)
        {
            throw new NotImplementedException();
        }

        public void StopUnit(EventHandler modeChangesHandler)
        {
            throw new NotImplementedException();
        }
    }

    public class ExploreModeController : ConsoleModeController
    {
        public override void HideMode()
        {
            throw new System.NotImplementedException();
        }

        public override void InitMode()
        {
            throw new System.NotImplementedException();
        }

        public override void ShowMode()
        {
            throw new System.NotImplementedException();
        }
    }
}

