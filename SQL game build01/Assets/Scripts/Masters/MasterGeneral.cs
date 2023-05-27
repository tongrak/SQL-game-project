using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterGeneral
{
    public class FailToGetUnityObjectException : System.Exception
    {
        public FailToGetUnityObjectException()
        {
        }

        public FailToGetUnityObjectException(string message) : base(message)
        {
        }
    }

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
        public static T GetObjectWithType<T>() where T : UnityEngine.Object
        {
            T buffer = UnityEngine.Object.FindAnyObjectByType<T>();
            if (buffer) return buffer;
            else throw new FailToGetUnityObjectException( string.Format("Cann't find object with type({0})", typeof(T).ToString()));
        }
    }
}
