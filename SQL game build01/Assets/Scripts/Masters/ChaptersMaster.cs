
using GameHelper;
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
        [Header("Configure value")]
        [SerializeField] private string _chapterFolderName = "Chapters";
        [SerializeField] private int _firstChapterIndex = 0;
        //Dynamic object
        private ChapterInterpreter _ChapI = ChapterInterpreter.Instance;
        private SceneLoadingHelper _SLH; //init during start;
        //Config var
        private string _defaultDirPath;
        private string[] _chapterRefPaths;
        private ChapterRef[] _chapterRefs;
        //Dynamic var
        private ChapterRef _currChapter = null;
        private RoomRef _currRoom = null;
        private RoomDirection _spawnDirection;

        #region Chapter Master Init

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

        #endregion

        #region Chapter Loading
        public void LoadStartOfChapter(int chapterIndex)
        {
            LoadStartOfChapter(_chapterRefs[chapterIndex]);
        }
        private void LoadStartOfChapter(ChapterRef chap)
        {
            _currChapter = chap;
            GoToRoom(chap.GetFirstRoom());
        }
        #endregion

        #region Room Loading
        public void GoToNeigborRoom(RoomDirection direction)
        {
            if (_currRoom == null) throw new System.Exception("Current room didn't initialize");

            string targetRoomName = _currRoom.GetNeighborInDirection(direction);
            RoomRef targetRoom = _currChapter.GetRoomRef(targetRoomName);

            if (targetRoom == null) throw new System.Exception("room(" + targetRoomName + ") doesn't existed in the chapter(" + _currChapter.name + ")");
            else
            {
                _spawnDirection = GetOppositeDi(direction);
                GoToRoom(targetRoom);
            }
        }
        public void UnloadCurrentRoom()
        {
            if (_currRoom != null)
            {
                _SLH.UnloadScene(_currRoom.name);
                _currRoom = null;
            }
        }
        private void GoToRoom(RoomRef room)
        {
            if (_currRoom == null) _SLH.LoadSceneAdditively(room.name);
            else _SLH.SwapScene(_currRoom.name, room.name);
            _currRoom = room;
        }
        #endregion

        #region Misc Function

        private RoomDirection GetOppositeDi(RoomDirection direction)
        {
            switch (direction)
            {
                case RoomDirection.UP: return RoomDirection.DOWN;
                case RoomDirection.RIGHT: return RoomDirection.LEFT;
                case RoomDirection.DOWN: return RoomDirection.UP;
                case RoomDirection.LEFT: return RoomDirection.RIGHT;
            }
            throw new System.Exception(direction + " isn't a invalid direction");
        }

        #endregion

        void Start()
        {
            _SLH = FindAnyObjectByType<SceneLoadingHelper>();
            //Init var
            _defaultDirPath = string.Format(Application.dataPath+@"\{0}",_chapterFolderName);
            _chapterRefPaths = GetChapterFileFrom(_defaultDirPath);
            _chapterRefs = GetChapRefFromPaths(_chapterRefPaths);

            //Load first default chapter
            LoadStartOfChapter(_firstChapterIndex);
        }
    }
}

