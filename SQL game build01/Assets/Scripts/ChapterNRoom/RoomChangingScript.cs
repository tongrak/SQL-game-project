using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChapNRoom
{
    public class RoomChangingScript : MonoBehaviour
    {
        [SerializeField] private RoomDirection _direction;
        public RoomDirection travelDirection { get { return _direction; } }

    }
}


