using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChapNRoom
{
    //public delegate Room

    public class RoomMaster : MonoBehaviour
    {
        [Header("Default Spawn Points")]
        [SerializeField] private GameObject _defaultSpawnPoint;
        [Header("Border Spawn Points")]
        [SerializeField] private GameObject _UpSpawnPoint;
        [SerializeField] private GameObject _RightSpawnPoint;
        [SerializeField] private GameObject _DownSpawnPoint;
        [SerializeField] private GameObject _LeftSpawnPoint;
        [Header("Player Holder")]
        [SerializeField] private GameObject _playerHolderObj;

        private List<GameObject> _spawnPoints = new List<GameObject>();

        public GameObject playerHolder { get { return _playerHolderObj; } }

        /// <summary>
        /// Get spawn point location in given direction. If given direction doesn't exist return default spawn point.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public GameObject GetSpawnPoint(RoomDirection? direction)
        {
            if(!direction.HasValue) return _defaultSpawnPoint;
            else
            {
                GameObject targetSpawn = _spawnPoints[(int)direction.Value];
                if (targetSpawn != null) return targetSpawn;
                Debug.LogWarning(string.Format("RoomMaster: No spawnpoint for direction: {0}", direction.Value));
                return _defaultSpawnPoint;
            }
        }

        private void Awake()
        {
            //Initialize
            _spawnPoints.Add(_UpSpawnPoint);
            _spawnPoints.Add(_RightSpawnPoint);
            _spawnPoints.Add(_DownSpawnPoint);
            _spawnPoints.Add(_LeftSpawnPoint);

            if (_spawnPoints.TrueForAll(x => x == null)) throw new System.Exception("Fail to load the room");
        }
    }
}


