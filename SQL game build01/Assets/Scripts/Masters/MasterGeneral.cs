using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterGeneral
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

    public static class MasterHelper
    {
        public static T GetMasterWithType<T>() where T : UnityEngine.Object
        {
            T buffer = UnityEngine.Object.FindAnyObjectByType<T>();
            if (buffer) return buffer;
            else throw new System.Exception("Cann't find  master with type" + typeof(T).ToString());
        }


    }
}
