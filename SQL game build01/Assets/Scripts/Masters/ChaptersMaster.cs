using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Script to control a loading of active gameplay scene or room in short.
 */
namespace ChapNRoom
{
    public class ChaptersMaster : MonoBehaviour
    {
        [SerializeField] private string _chapterFolderName = "Chapters";

        private ChapterInterpreter _ChapI = ChapterInterpreter.Instance;

        //Config var
        private string _defaultDirPath;
        private string[] _chapterRefPaths;
        private ChapterRef[] _chapterRefs;

        private string[] GetChapterFileFrom(string chapterRefsPath)
        {
            //try getting all txt files from given path.
            return Directory.GetFiles(chapterRefsPath, "*.txt");

        }

        private ChapterRef[] GetChapRefFromPaths(string[] chapRefPaths)
        {
            List<ChapterRef> chaptersBuffer = new List<ChapterRef>();

            foreach (string path in chapRefPaths)
            {
                try
                {
                    chaptersBuffer.Add(_ChapI.GetChapRefFrom(path));
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning("Cann't add chapter from :" +  path + " ; Due to: " + ex.ToString()); 
                }
            }

            return chaptersBuffer.ToArray();
        }

        //private void 

        void Start()
        {
            //Init var
            _defaultDirPath = string.Format(Application.dataPath+@"\{0}",_chapterFolderName);
            _chapterRefPaths = GetChapterFileFrom(_defaultDirPath);
            _chapterRefs = GetChapRefFromPaths(_chapterRefPaths);

           

            //Dummy line
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
    }
}

