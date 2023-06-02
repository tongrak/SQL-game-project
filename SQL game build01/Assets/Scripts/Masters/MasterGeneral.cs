using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public struct RoomSpawningDetail
    {
        public GameObject spawn { get; }
        public GameObject playerholder { get; }
        public RoomSpawningDetail(GameObject spawnLocation, GameObject playerholder)
        {
            this.spawn = spawnLocation;
            this.playerholder = playerholder;
        }
    }
}
