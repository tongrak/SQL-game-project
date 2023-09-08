using System;
using System.Collections.Generic;
using System.Linq;
using TarodevController;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlMovement : MonoBehaviour
    {
        #region Movement
        private Rigidbody2D _rigidbody;

        [Header("Movement Config")]
        [SerializeField][Range(0, 100f)] private float _walkSpeed = 10f;
        [SerializeField] private float _jumpSpeed = 10f;

        private void MoveCharacter(MovementInput mInput, FourDirections<bool> collideOn)
        {
            MoveVertically(mInput.JumpPressDown, collideOn.down);
            MoveHorizontally(mInput.Horizontal, collideOn.left, collideOn.right);
        }

        private void MoveHorizontally(float xSignal, bool collidOnLeft, bool collidOnRight)
        {
            if (xSignal < 0 && collidOnLeft || xSignal > 0 && collidOnRight) return;
            // TODO: when not grounded speed should be different
            _rigidbody.velocity = new Vector2(xSignal * _walkSpeed, _rigidbody.velocity.y);
        }

        private void MoveVertically(bool pressJump, bool grounded)
        {
            if (!(pressJump && grounded)) return;
            // TODO: Add buffered jump

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
        }

        #endregion

        #region Collision
        private BoxCollider2D _boxCollider;
        private Bounds _bounds => _boxCollider.bounds;

        [Header("Collision")]
        [SerializeField] private LayerMask _platformLayer;

        private FourDirections<bool> getFourDirectionCollision() =>
            FourDirections<bool>.Convert(CommonVariable.defaultDirections.Select<Vector2, bool>(
                dic => Physics2D.BoxCast(_bounds.center, _bounds.size, 0, dic, .1f, _platformLayer)
                )
            );
        #endregion

        #region Unity Basic

        private void Start()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // Gathering Input
            var playerInput = new MovementInput
            {
                JumpPressDown = UnityEngine.Input.GetButtonDown("Jump"),
                Horizontal = UnityEngine.Input.GetAxisRaw("Horizontal"),
            };
            // Check collision
            var collisions = getFourDirectionCollision();

            MoveCharacter(playerInput, collisions);

            StatusLoging();
        }

        private void StatusLoging()
        {
        }


        #endregion
    }
}