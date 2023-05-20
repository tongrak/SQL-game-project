using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChapNRoom
{
    public class RoomMaster : MonoBehaviour
    {
        [Header("Default Spawn Points")]
        [SerializeField] private GameObject _defaultSpawnPoint;
        [Header("Border Spawn Points")]
        [SerializeField] private GameObject _UpSpawnPoint;
        [SerializeField] private GameObject _RightSpawbPoint;
        [SerializeField] private GameObject _DownSpawnPoint;
        [SerializeField] private GameObject _LeftSpawbPoint;
        [Header("Player Holder")]
        [SerializeField] private GameObject _playerHolderObj;

        /// <summary>
        /// Get spawn point location in given direction. If given direction doesn't exist return default spawn point.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public GameObject GetSpawnPointInDirection(RoomDirection direction)
        {
            GameObject[] _spawnPoints = { _UpSpawnPoint, _RightSpawbPoint, _DownSpawnPoint, _LeftSpawbPoint };
            GameObject targetSpawn = _spawnPoints[(int)direction];
            if (targetSpawn == null) return _defaultSpawnPoint;
            else return targetSpawn;
        }

        public GameObject GetPlayerHolder()
        {
            return _playerHolderObj;
        }

        public GameObject GetDefaultSpawnPoint()
        {
            return _defaultSpawnPoint;
        }
    }
}


