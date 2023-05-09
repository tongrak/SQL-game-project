
using System;
using System.Collections.Generic;
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
            catch (Exception e) { throw new Exception("Wrong chapter format: " + e.ToString()); }
        }

        private RoomRef CreateRoomRef(string roomDetail)
        {
            Tuple<string,string> headNBody = StringHelper.SpliteByPivot(":", roomDetail);
            if (headNBody == null) throw new Exception("no colon in roomdetail: " + roomDetail);
            if (String.IsNullOrEmpty(headNBody.Item1) || String.IsNullOrEmpty(headNBody.Item2)) throw new Exception("HeadOrBody is empty");
            string[] neigbors = headNBody.Item2.Split(",");
            if (neigbors.Length != 4) throw new Exception("Invalid number of neigbors");

            return new RoomRef(headNBody.Item1, neigbors);
        }

        public ChapterRef GetChapRefFrom(string chapName, string path)
        {
            if (File.Exists(path))
            {
                string inTexts = File.ReadAllText(path);
                inTexts.Trim();
                string roomsDetails = StringHelper.GetStringBetween("rooms{", "}", inTexts);
                RoomRef[] roomRefs = GetRoomRefs(inTexts);
                return new ChapterRef(chapName, roomRefs);
            }
            else throw new FileNotFoundException("Cann't find chapter reference file in: " + path);
        }

    }
}