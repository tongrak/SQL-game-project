using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    class GameplayUtil
    {
        public readonly static Vector2[] defaultDirections = new Vector2[4] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    }

    struct MovementInput
    {
        public bool JumpPressDown;
        public bool? JumpPressUp;
        public float Horizontal;
    }

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

        public static FourDirections<T> Convert(IEnumerable<T> given)
        {
            if (given.Count() != 4) throw new ArgumentException("Cannot convert non 4 member IEnumerable");
            return new FourDirections<T>(given.ElementAt(0), given.ElementAt(1), given.ElementAt(2), given.ElementAt(3));
        }
    }

    class FourDirectionEnum<T> : IEnumerator<T>
    {
        public T[] _fourDirectedVal;
        int position = -1;

        public FourDirectionEnum(T[] fourDirectedVal) => _fourDirectedVal = fourDirectedVal;

        public T Current
        {
            get
            {
                try
                {
                    return _fourDirectedVal[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

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

    #region Animation

    enum PlCharState
    {
        IDLE, WALK, JUMP
    }

    interface IPlAnimationCtr
    {
        /// <summary>
        /// Handle charector sprite horizonal fliping
        /// </summary>
        /// <param name="xSignal">Player horizontal control signal</param>
        void HorizontalAct(float xSignal);

        /// <summary>
        /// Handle charector animation base on given state
        /// </summary>
        /// <param name="currState"></param>
        void ChangeAnimateState(PlCharState currState);

    }

    #endregion

    #region Utility

    #endregion

    public abstract class GameplayBaseScript : MonoBehaviour
    {
        protected T MustGetComponent<T>()
        {
            var temp = GetComponent<T>();
            if (temp != null) return temp;
            throw new NullReferenceException($"Cannot find component with type: {typeof(T)}");
        }
    }
}