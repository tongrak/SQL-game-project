using System;

namespace GameHelper
{
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