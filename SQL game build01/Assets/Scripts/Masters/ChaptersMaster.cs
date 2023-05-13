using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Script to control a loading of active gameplay scene or room in short.
 */
namespace ChapNRoom
{
    public class ChaptersMaster : MonoBehaviour
    {

        //private ChapInterpreterInter _ChapI;
        

        void Start()
        {
            //Dummy line
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
    }
}

