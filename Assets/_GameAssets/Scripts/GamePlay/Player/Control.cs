using UnityEngine;
using System;
public class Control
    : MonoBehaviour
{
 


    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")]
    [SerializeField] private KeyCode _moveKey;
    [SerializeField] private float _movementSpeed;

    [Header("Slide Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    [SerializeField] private bool _canJump;

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _floorLayer;
    [SerializeField] private float _playerHeight;
    [SerializeField] private float _groundDrag;

    private float _horizontalInput, _verticalInput;
    private float _startingMovementSpeed, _startingJumpForce;
    private bool _isSliding = false;
    private Vector3 _movementDirection;
    private Rigidbody _playerRigidbody;







    private void Awake()
    {
        _canJump = true;
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;

        _startingMovementSpeed = _movementSpeed;
        _startingJumpForce = _jumpForce;
    }

    private void Update()
    {
       

        SetInputs();
 
        SetPlayerSpeed();
        SetPlayerDrag();
    }

    private void FixedUpdate()
    {
       

        SetPlayerMovement();
    }



    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false; 
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }


    }

  

    public void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;

       

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed , ForceMode.Force);
    }

    private void SetPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void SetPlayerDrag()
    {
        if (IsGrounded())
        {
            _playerRigidbody.linearDamping = _groundDrag;
        }
        else
        {
            _playerRigidbody.linearDamping = _airDrag;
        }
    }



    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }


    public bool IsSliding()
    {
        return _isSliding;
    }

    private void ResetJumping()
    {
        _canJump = true;
    }

    public Vector3 GetMovementDirection()
    {
        return _movementDirection.normalized;
    }

    public void SetMovementSpeed(float speed, float duration)
    {
        _movementSpeed += speed;
        Invoke(nameof(ResetMovementSpeed), duration);
    }

    private void ResetMovementSpeed()
    {
        _movementSpeed = _startingMovementSpeed;
    }

    public void SetJumpForce(float force, float duration)
    {
        _jumpForce += force;
        Invoke(nameof(ResetJumpForce), duration);
    }

    private void ResetJumpForce()
    {
        _jumpForce = _startingJumpForce;
    }

    public Rigidbody GetPlayerRigidbody()
    {
        return _playerRigidbody;
    }
}
