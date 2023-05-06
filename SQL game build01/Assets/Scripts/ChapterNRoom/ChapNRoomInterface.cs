
namespace ChapNRoom
{
    //Room's neighbor direction base on 4-side clock wise direction.
    public enum RoomDirection {UP, RIGHT, DOWN, LEFT}

    
    //Room reference: reference of a room. Holding the room's name and it's neighbors name.
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
    }

    //Chapter reference: reference of a chapter. Holdering all room reference of the chapter.
    public class ChapterRef
    {
        public string name { get; }
        private RoomRef[] _roomRefs;

        public ChapterRef(string name, RoomRef[] roomRefs)
        {
            this.name = name;
            _roomRefs = roomRefs;
        }

        public RoomRef GetRoomRef(string roomName)
        {
            foreach (RoomRef curr in this._roomRefs)
            {
                if (curr.name == roomName) return curr;
            }
            return null;
        }

    }

    //An interface for chapters interpreter. If chapters path provided return chapters refernce of that path else return chapters reference of default path
    //(..\..\Chapters)
    public interface ChapInterpreterInterface
    {
        //get an instance of Chapter interpretor
        ChapterRef[] GetChapRefFrom(string path);
        ChapterRef[] GetChapRefFrom();

    }

}