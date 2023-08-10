using Gameplay;
using Gameplay.ChaptersNRooms;
using Gameplay.ChaptersNRooms.Interpreter;
using Gameplay.Helper;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public delegate void RoomLoadedHandler(RoomSpawningDetail spawningDetail);

/*
Script to control a loading of active gameplay scene or room in short.
 */
namespace Gameplay.Manager
{
    public class ChaptersManager : MonoBehaviour
    {
        [Header("Configure value")]
        [SerializeField] private string _chapterFolderName = "ChaptersNRooms";
        [SerializeField] private int _firstChapterIndex = 0;
        //Dynamic object
        private ChapterInterpreter _ChapI = ChapterInterpreter.Instance;
        private SceneLoadingHelper _SLH; //init during start;
        private RoomMaster _currRoomMaster;
        //Config var
        private string _defaultDirPath;
        private string[] _chapterRefPaths;
        private ChapterRef[] _chapterRefs;
        //Dynamic var
        private ChapterRef _currChapter = null;
        private RoomRef _currRoom = null;
        private RoomSpawningDetail _spawningDetail;

        public event RoomLoadedHandler RoomLoaded;

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
            GoToRoom(chap.GetFirstRoom(),null);
        }
        #endregion

        #region Room Loading
        /// <summary>
        /// Switch to neigbor room of given direction (if existed) and return spawn location in Vector2 manner.
        /// </summary>
        /// <param name="direction">Direction of the neigbor</param>
        /// <returns>Vector2 of spawn location</returns>
        /// <exception cref="System.Exception">If no neigbor in given direction</exception>
        public void GoToNeigborRoom(RoomDirection direction)
        {
            //if (_currRoom == null) throw new System.Exception("Current room didn't initialize");

            string targetRoomName = _currRoom.GetNeighborInDirection(direction);
            RoomRef targetRoom = _currChapter.GetRoomRef(targetRoomName);

            if (targetRoom == null) throw new System.Exception("room(" + targetRoomName + ") doesn't existed in the chapter(" + _currChapter.name + ")");
            else
            {
                RoomDirection spawnDirection = GetOppositeDi(direction);
                GoToRoom(targetRoom, spawnDirection);
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
        private void GoToRoom(RoomRef room, RoomDirection? direction)
        {
            if (_currRoom == null) _SLH.LoadSceneAdditively(room.name);
            else _SLH.SwapScene(_currRoom.name, room.name);

            if(direction.HasValue) _SLH.ActOnSceneSwaped(_currRoom.name, room.name, () => CallForPlayerSpawn(direction));
            else _SLH.ActOnSceneLoaded(room.name, () => CallForPlayerSpawn(direction));

            _currRoom = room;
        }
        #endregion

        #region Misc Function
        private void CallForPlayerSpawn(RoomDirection? direction)
        {
            RoomMaster currRoomMaster = FindAnyObjectByType<RoomMaster>();
            if (!currRoomMaster) throw new System.Exception("Fail to get room master");
            GameObject spawnLocation = currRoomMaster.GetSpawnPoint(direction);
            GameObject playerHolder = currRoomMaster.playerHolder;
            RoomSpawningDetail spawningDetail = new RoomSpawningDetail(spawnLocation, playerHolder);

            this.spawnPlayer(spawningDetail);
        }
        private RoomDirection GetOppositeDi(RoomDirection direction)
        {
            RoomDirection[] reverseDirection = { RoomDirection.DOWN, RoomDirection.LEFT, RoomDirection.UP, RoomDirection.RIGHT };
            return reverseDirection[(int)direction];
        }

        public virtual void spawnPlayer(RoomSpawningDetail spawningDetail)
        {
            RoomLoaded?.Invoke(spawningDetail);
        }

        #endregion

        #region Unity Basic
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
        #endregion
    }
}