using System;
using System.Collections.Generic;
using System.Linq;
using TarodevController;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlMovement : MonoBehaviour
    {
        [Header("Basic ConFig")]

        private Vector3 _lastPosition;
        private Vector3 _velocity;
        // TODO: Fix air hockey movement when swap direction quickly.
        #region Horizontal movement
        private float _currHorizontalSpeed;
        [Header("Horizontal")]
        [SerializeField] private float _acceleration = 1f;
        [SerializeField] private float _deAcceleration = 60f;
        [SerializeField] private float _maxClamp = 150;

        private FrameInput _playerInput;
        void CalculateWalkRelated()
        {
            if(_playerInput.X != 0) {
                _currHorizontalSpeed += _playerInput.X * _acceleration * Time.deltaTime;
                _currHorizontalSpeed = Mathf.Clamp(_currHorizontalSpeed, -_maxClamp, _maxClamp);
            } else { 
                _currHorizontalSpeed = Mathf.MoveTowards(_currHorizontalSpeed, 0, _deAcceleration * Time.deltaTime);
            }
            if (_currHorizontalSpeed > 0 && _collideOn.right || _currHorizontalSpeed < 0 && _collideOn.left) _currHorizontalSpeed = 0;
        }
        #endregion
        // TODO: Falling abit too fast.
        #region Vertical movement

        //public bool jumpThisFrame { get; private set; }
        //public bool landingThisFrame { get; private set; }

        private float _lastJumpPressed;

        [Header("Vertical")]
        // TODO: Add end jump early if button ?
        // TODO: Coyote time
        //[SerializeField] private float _jumpForce = 300;
        [SerializeField] private float _jumpHeight = 30;
        [SerializeField] private float _jumpApexThreshold = 10f;
        //private float JUMP_BUFFER = 0.1f;

        private float _apexPoint;
        [SerializeField] private float _minFallSpeed = 80f;
        [SerializeField] private float _maxFallSpeed = 120f;
        [SerializeField] private float _fallCramp = 40f;
        private float _fallSpeed;
        private float _currVerticalSpeed;
        private void CalculateFallSpeed(){
            if (!_collideOn.down){
                _apexPoint = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(_velocity.y));
                _fallSpeed = Mathf.Lerp(_minFallSpeed, _maxFallSpeed, _apexPoint);
            }
            else {
                _apexPoint = 0;
                _fallSpeed = 0;
            }
        }
        private void CalculateJumpSpeed(){
            if (_playerInput.JumpDown && _collideOn.down)
            {
                _currVerticalSpeed = _jumpHeight;
                //this.jumpThisFrame = true;
            }else{
                //this.jumpThisFrame= false;
            }
            // Stop if head hit a platform.
            if (_collideOn.up && _currVerticalSpeed > 0){
                _currVerticalSpeed = 0;
            }
        }
        private void CalculateGravity()
        {
            if (_collideOn.down && _currVerticalSpeed < 0) _currVerticalSpeed = 0;
            else { 
                _currVerticalSpeed -= _fallSpeed * Time.deltaTime;
                if (_currVerticalSpeed < -_fallCramp) _currVerticalSpeed = -_fallCramp;
            }
        }
        #endregion

        #region Enforcer
        [SerializeField] private int _freeColliderIterations = 10;
        private void MoveCharacter(){
            var currPos = transform.position + _characterBounds.center;
            var rawMovementVector = new Vector3(_currHorizontalSpeed, _currVerticalSpeed);
            var movementVector = rawMovementVector * Time.deltaTime;
            var nextFramePos = currPos + movementVector;
            //Move to furtherest if nothing hit
            var hitPlatform = Physics2D.OverlapBox(nextFramePos, _characterBounds.size, 0, _platformLayer);
            if (!hitPlatform){
                transform.position += movementVector;
                return;
            }
            //else slowly move till collide
            var posToMove = transform.position;
            for (var i = 1; i < _freeColliderIterations; i++) { 
                var t = (float) i/ _freeColliderIterations;
                var posToTry = Vector2.Lerp(currPos, nextFramePos, t);

                if(Physics2D.OverlapBox(posToTry, _characterBounds.size, 0, _platformLayer)) { 
                    transform.position = posToMove;

                    if (i == 1 && _currVerticalSpeed < 0) _currVerticalSpeed = 0;
                }
                posToMove = posToTry;
            }
        }

        #endregion

        #region Collision
        [Header("Collision")]
        [SerializeField] private Bounds _characterBounds;
        [SerializeField] private LayerMask _platformLayer;

        [SerializeField] private int _detectionCount = 3;
        [SerializeField] private float _rayBuffer = 0.1f;
        [SerializeField] private float _rayDetectionLength = 1.0f;

        private FourDirections<RayRange> _rayOn;
        private FourDirections<bool> _collideOn = new FourDirections<bool>(false, false, false, false);
        //private float _time

        private void UpdateCollision()
        {
            CalculatedRayRanged();
            //this.landingThisFrame = false;

            var groundedThisFrame = CheckIfCollide(_rayOn.down);
            //if (!_collideOn.down && groundedThisFrame) this.landingThisFrame = true;
            _collideOn.down = groundedThisFrame;
            _collideOn.right = CheckIfCollide(_rayOn.right);
            _collideOn.up = CheckIfCollide(_rayOn.up);
            _collideOn.left = CheckIfCollide(_rayOn.left);
        }

        private bool CheckIfCollide(RayRange ray) => 
            PredictRayPositions(ray).Any(p => Physics2D.Raycast(p, ray.Dir, _rayDetectionLength, this._platformLayer));

        private IEnumerable<Vector2> PredictRayPositions(RayRange range)
        {
            for(var i = 0; i < _detectionCount; i++){
                var t = (float) i / (_detectionCount - 1);
                yield return Vector2.Lerp(range.Start, range.End, t);
            }
        }

        private void CalculatedRayRanged()
        {
            //Player's bounds
            Bounds b  = new Bounds(transform.position + _characterBounds.center, _characterBounds.size);

            RayRange raysDown = new RayRange(b.min.x + _rayBuffer, b.min.y, b.max.x - _rayBuffer, b.min.y, Vector2.down);
            RayRange raysUp = new RayRange(b.min.x + _rayBuffer, b.max.y, b.max.x - _rayBuffer, b.max.y, Vector2.up);
            RayRange raysLeft = new RayRange(b.min.x, b.min.y + _rayBuffer, b.min.x, b.max.y - _rayBuffer, Vector2.left);
            RayRange raysRight = new RayRange(b.max.x, b.min.y + _rayBuffer, b.max.x, b.max.y - _rayBuffer, Vector2.right);
            _rayOn = new FourDirections<RayRange>(raysUp, raysRight, raysDown, raysLeft);
        }
        #endregion

        #region Unity Basic
        // Avoid uninitilize colliders.
        private bool _active;
        void Awake() => Invoke(nameof(ActivateThis), 0.5f);
        void ActivateThis() => _active = true;

        void Update()
        {
            if (!_active) return ;
            _velocity = (transform.position - _lastPosition) / Time.deltaTime;
            _lastPosition = transform.position;

            // Gathering Input
            _playerInput = new FrameInput {
                JumpDown = UnityEngine.Input.GetButtonDown("Jump"),
                JumpUp = UnityEngine.Input.GetButtonUp("Jump"),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
            };
            if (_playerInput.JumpDown) _lastJumpPressed = Time.time;

            UpdateCollision();

            // Side-effect functions
            CalculateWalkRelated();
            CalculateFallSpeed();
            CalculateGravity();
            CalculateJumpSpeed();

            MoveCharacter();

            //StatusLoging();
        }

        private void StatusLoging()
        {
            Debug.Log("Pl Movement:");
            Debug.Log("Collision:" + _collideOn.ToString());
            Debug.Log($"Input: [horrizontal:{_playerInput.X.ToString()}, vertical:{_playerInput.JumpDown}]");
            if (_currVerticalSpeed != 0) Debug.Log($"Vertical speed: {_currVerticalSpeed}");
            if (_currHorizontalSpeed != 0) Debug.Log($"Horizontal speed: {_currHorizontalSpeed}");
            Logger.LogIf(_apexPoint != 0, $"Apex point: {_apexPoint}");
            Logger.LogIf(_fallSpeed != 0, $"Fallspeed point: {_fallSpeed}");
        }


        #endregion
    }
}