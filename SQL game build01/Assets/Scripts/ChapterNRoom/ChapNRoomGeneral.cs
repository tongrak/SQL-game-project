
using System;

namespace ChapNRoom
{
    /// <summary>
    /// Room's neighbor. Direction base on 4-side clock wise direction.
    /// </summary>
    public enum RoomDirection { UP, RIGHT, DOWN, LEFT }

    /// <summary>
    ///  Reference of a room. Holding the room's name and it's neighbors name.
    /// </summary>
    public class RoomRef
    {
        public string name { get; }

        private string[] _neighbors = new string[4];

        public RoomRef(string name, string[] neighbors)
        {
            this.name = name;
            this._neighbors = neighbors;
        }

        public string GetNeighborInDirection(RoomDirection direction)
        {
            return this._neighbors[(int)direction];
        }

        public override string ToString()
        {
            return "Room(" + name + "):" + _neighbors.ToString() + ";";
        }
    }

    /// <summary>
    /// Reference of a chapter. Holdering all room reference of the chapter.
    /// </summary>
    public class ChapterRef
    {
        public string name { get; }
        private RoomRef[] _roomRefs;

        public ChapterRef(string name, RoomRef[] roomRefs)
        {
            this.name = name;
            _roomRefs = roomRefs;
        }
        /// <summary>
        /// Get Room reference with a given name of the chapter.
        /// </summary>
        /// <param name="roomName">Room name to be searched</param>
        /// <returns>if given name exist in the chapter return RoomRef, else null </returns>
        public RoomRef GetRoomRef(string roomName)
        {
            foreach (RoomRef curr in this._roomRefs)
            {
                if (curr.name == roomName) return curr;
            }
            return null;
        }

        public RoomRef GetFirstRoom()
        {
            return _roomRefs[0];
        }

        public override string ToString()
        {
            return "Chapter(" + name + "):" + _roomRefs.ToString() + ";";
        }


    }

    /// <summary>
    /// An interface for chapter interpreter. 
    /// </summary>
    public interface ChapInterpreterInterface
    {
        //get an instance of Chapter interpretor
        ChapterRef GetChapRefFrom(string chapName, string path);
    }
}