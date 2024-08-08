using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _groundRaycastTransform;
    [SerializeField] private LayerMask _groundLayer;

    private float _horizontalMovement;
    private float _movementSpeed = 3f;
    private float _jumpForce = 6.5f;
    private float _jumpResetHeight = 0.5f;
    private bool _isGrounded = false;
    private bool _isFacingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
        }

        // Code for a potential scalable jump if scope allows it
        //if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0f)
        //{
        //    _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y * 0.5f, _rb.velocity.z);
        //}

        ChangeFacingDirection();

        CheckIsGrounded();
    }

    private void FixedUpdate()
    {
        // Move the player
        _rb.velocity = new Vector3(_horizontalMovement * _movementSpeed, _rb.velocity.y, _rb.velocity.z);
    }

    // Face the player the opposite direction when directional input switches
    private void ChangeFacingDirection()
    {
        if (_isFacingRight && _horizontalMovement < 0f || !_isFacingRight && _horizontalMovement > 0f) 
        {
            // Flip player local scale
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Check how close the player is to the ground
    private void CheckIsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(_groundRaycastTransform.position, Vector3.down, out hit, _jumpResetHeight))
        {
            Console.WriteLine("Casting");

            _isGrounded = true;
            return;
        }

        _isGrounded = false;
    }
}
