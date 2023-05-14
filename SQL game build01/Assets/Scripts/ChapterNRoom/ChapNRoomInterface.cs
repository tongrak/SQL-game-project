
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

        public RoomRef GetRoomRef(string roomName)
        {
            foreach (RoomRef curr in this._roomRefs)
            {
                if (curr.name == roomName) return curr;
            }
            return null;
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

    public static class StringHelper
    {
        #region Helper
        /// <summary>
        /// Return a string between fstString and sndString that first occur.
        /// </summary>
        /// <param name="fstString">First string</param>
        /// <param name="sndString">Second string</param>
        /// <param name="inString">Given string</param>
        /// <returns>return string if fstString and sndString exist else return null</returns>
        public static string GetStringBetween(string fstString, string sndString, string inString)
        {
            if (inString.Contains(fstString) && inString.Contains(sndString))
            {
                string hairOff = inString.Remove(0, inString.IndexOf(fstString));
                string headOff = hairOff.Remove(0, fstString.Length);
                string feetOff = headOff.Remove(headOff.IndexOf(sndString));
                return feetOff;
            }
            else return null;
        }
        /// <summary>
        /// Return a pair which fst is a string that come before pivot and snd is the remainding.
        /// </summary>
        /// <param name="pivot">String pivot</param>
        /// <param name="inString">String to be splited</param>
        /// <returns>A Pair if pivot existed else return null </returns>
        public static Tuple<string, string> SpliteByPivot(string pivot, string inString)
        {
            if (inString.Contains(pivot))
            {
                string fstString = inString.Clone().ToString().Remove(inString.IndexOf(pivot));
                string scdString = inString.Clone().ToString().Remove(0, inString.IndexOf(pivot) + 1);
                return new Tuple<string, string>(fstString, scdString);
            }
            else return null;
        }

        #endregion
    }


}