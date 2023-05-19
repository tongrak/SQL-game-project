using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlMovement : MonoBehaviour
{
    [Header("Basic ConFig")]
    public Rigidbody2D rb2D;


    // Update is called once per frame
    void Update()
    {
        CheckJump();

        WalkCal();

        Move();
    }

    #region Horizontal movement
    private float _currWalkSpeed;
    [Header("Horizontal")]
    [SerializeField] private float _acceleration = 1f;
    [SerializeField] private float _deAcceleration = 60F;
    [SerializeField] private float _maxClamp = 150;
    void WalkCal()
    {
        float xInput = UnityEngine.Input.GetAxis("Horizontal");

        if (xInput == 0)
        {
            _currWalkSpeed = Mathf.MoveTowards(_currWalkSpeed, 0, _deAcceleration * Time.deltaTime);
        }
        else 
        {
            _currWalkSpeed = xInput * _acceleration * Time.deltaTime;
            _currWalkSpeed = Mathf.Clamp(_currWalkSpeed, -_maxClamp, _maxClamp);
        }
    }
    #endregion

    #region Vertical movement

    private bool _canJump = false;
    private bool _bufferedJump = false;
    [Header("Vertical")]
    [SerializeField] private float _jumpForce = 300;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground") _canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && _bufferedJump) Jump();
        else _canJump = true;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground") _canJump = true;
    }

    void CheckJump(){
        bool yInput = UnityEngine.Input.GetButtonDown("Jump");
        if (yInput)
        {
            if (_canJump) Jump();
            else _bufferedJump = true;
        }
    }

    void Jump()
    {
        rb2D.AddForce(transform.up * _jumpForce);
        _canJump = false;
        _bufferedJump = false;
    }
    #endregion

    #region Enforcer
    void Move()
    {
        transform.position += new Vector3(_currWalkSpeed, 0);
    }

    #endregion
}
