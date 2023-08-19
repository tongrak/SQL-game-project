using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gameplay
{
    class FourDirections<T> : IEnumerable<T>
    {
        public T up; public T right; public T down; public T left;
        public T[] asList => new T[4] { up, right, down, left };

        public FourDirections(T upVal, T rightVal, T downVal, T leftVal)
        {
            up = upVal;
            right = rightVal;
            down = downVal;
            left = leftVal;
        }   

        public IEnumerator<T> GetEnumerator()
        {
            return new FourDirectionEnum<T>(this.asList);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"[up:{up}, right:{right}, down:{down}, left:{left}]";
        }
    }

    class FourDirectionEnum<T> : IEnumerator<T>
    {
        public T[] _fourDirectedVal;
        int position = -1;

        public FourDirectionEnum(T[] fourDirectedVal) => _fourDirectedVal = fourDirectedVal;

        public T Current { get {
                try {
                    return _fourDirectedVal[position];
                } catch (IndexOutOfRangeException) {
                    throw new IndexOutOfRangeException();
                }
        } }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public bool MoveNext()
        {
            position++;
            return position < _fourDirectedVal.Length;
        }

        public void Reset()
        {
            position = -1;
        }
    }
    
    class Logger
    {
        public static void LogIf(bool condition,  string message) {
            if (condition) UnityEngine.Debug.Log(message);
        }
    }
}