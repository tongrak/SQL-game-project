using System.Collections;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlAnimationCtr : GameplayBaseScript, IPlAnimationCtr
    {
        private SpriteRenderer _sprite;
        private Animator _anim;

        private PlCharState _currState = PlCharState.IDLE;
        private PlCharState _currHorizontalState;
        private PlCharState _currVerticalState;

        void IPlAnimationCtr.ChangeAnimateState(PlCharState currState) => _currState = currState;

        void IPlAnimationCtr.HorizontalAct(float xSignal)
        {
            if (xSignal == 0)
            {
                _currHorizontalState = PlCharState.IDLE;
                return;
            }
            _currHorizontalState = PlCharState.WALK;
            _sprite.flipX = xSignal < 0;
        }

        void IPlAnimationCtr.VerticalAct(float ySignal)
        {
            if (ySignal == 0)
            {
                _currVerticalState = PlCharState.IDLE;
                return;
            }
            _currVerticalState = ySignal > 0 ? PlCharState.JUMP : PlCharState.FALL;
        }

        private void UpdateAnimation()
        {
            _currState = (_currVerticalState == PlCharState.IDLE) ? _currHorizontalState : _currVerticalState;


            if (_currState == PlCharState.FALL)
            {
                _anim.SetInteger("CharacterState", 2);
                return;
            }
            _anim.SetInteger("CharacterState", (int)_currState);
        }

        #region Unity Basic

        private void Start()
        {
            _anim = MustGetComponent<Animator>();
            _sprite = _anim.GetComponent<SpriteRenderer>();
        }

        private void Update() => UpdateAnimation();

        #endregion
    }
}