using UnityEngine;

namespace Gameplay.ChaptersNRooms
{
    public class RoomChangingScript : MonoBehaviour
    {
        [SerializeField] private RoomDirection _direction;
        public RoomDirection travelDirection { get => _direction; }

    }
}


