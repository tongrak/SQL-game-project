
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ChapNRoom
{
    public class ChapterInterpreter : ChapInterpreterInterface
    {
        //private string _inTexts = null;
        //Singleton varirable
        private ChapterInterpreter() { }
        private static ChapterInterpreter instance = null;
        //Singleton instance
        public static ChapterInterpreter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChapterInterpreter();
                }
                return instance;
            }
        }

        /// <summary>
        ///     Generate Chapter Reference (ChapterRef) from given path and Chapter name.
        /// </summary>
        /// <param name="path">file's full path in form of string</param>
        /// <param name="chapName">string represent chapter</param>
        /// <returns>If interpretation didn't got interrupted return ChapterRef else throw exception with message</returns>
        public ChapterRef GetChapRefFrom(string path, string chapName)
        {
            try
            {
                if (File.Exists(path))
                {
                    string inTexts = File.ReadAllText(path);
                    inTexts = inTexts.Trim();
                    inTexts = inTexts.Replace("\r\n", string.Empty);
                    string roomsDetails = StringHelper.GetStringBetween("Rooms{", "}", inTexts);
                    RoomRef[] roomRefs = GetRoomRefs(roomsDetails);
                    return new ChapterRef(chapName, roomRefs);
                }
                else throw new FileNotFoundException("Cann't find chapter reference file in: " + path);
            }
            catch (Exception e) { throw e; }
                
        }

        /// <summary>
        ///     Generate Chapter Reference (ChapterRef) from given path. Which the result Chapter reference will be named with file name.
        /// </summary>
        /// <param name="path">file's full path in form of string</param>
        /// <returns>If interpretation didn't got interrupted return ChapterRef else throw exception with message</returns>
        public ChapterRef GetChapRefFrom(string path)
        {
            return GetChapRefFrom(path, Path.GetFileName(path));
        }

        #region Misc Functions

        private RoomRef[] GetRoomRefs(string inString)
        {
            try
            {
                string[] roomsDetails = inString.Split(";");
                List<RoomRef> roomRefs = new List<RoomRef>();
                foreach (string room in roomsDetails)
                {
                    if (String.IsNullOrEmpty(room)) continue;
                    roomRefs.Add(CreateRoomRef(room));
                }
                return roomRefs.ToArray();
            }
            catch (Exception e) { throw new Exception("Wrong chapter format: " + e.Message); }
        }

        private RoomRef CreateRoomRef(string roomDetail)
        {
            Tuple<string, string> headNBody = StringHelper.SpliteByPivot(":", roomDetail);
            if (headNBody == null) throw new Exception("no colon in roomdetail: " + roomDetail);
            if (String.IsNullOrEmpty(headNBody.Item1) || String.IsNullOrEmpty(headNBody.Item2)) throw new Exception("HeadOrBody is empty");
            string[] neigbors = headNBody.Item2.Split(",");
            if (neigbors.Length != 4) throw new Exception("Invalid number of neigbors");

            return new RoomRef(headNBody.Item1, neigbors);
        }

        #endregion
    }
}