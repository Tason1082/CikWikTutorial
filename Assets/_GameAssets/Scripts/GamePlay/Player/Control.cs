using UnityEngine;
using System;
public class Control
    : MonoBehaviour
{


    public event Action OnPlayerJumped;
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


    private Control _playerController;
    [SerializeField] private Animator _playerAnimator;
    private StateController _stateController;



    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerController = GetComponent<Control>();
        _canJump = true;
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
       
        _startingMovementSpeed = _movementSpeed;
        _startingJumpForce = _jumpForce;
    }
    private void Start()
    {
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }
    private void PlayerController_OnPlayerJumped()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }
    private void Update()
    {
       

        SetInputs();
        SetStates();
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

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }
        else if (Input.GetKeyDown(_moveKey))
        {
            _isSliding = false;
        }
        else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false; 
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }


    }

    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !IsSliding() => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !IsSliding() => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && IsSliding() => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && IsSliding() => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            _stateController.ChangeState(newState);
            
        }
        
    }

    public void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Jump => _airMultiplier,
            PlayerState.Slide => _slideMultiplier,
            _ => 1f
        };

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
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
        OnPlayerJumped?.Invoke();
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void SetPlayerDrag()
    {
        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidbody.linearDamping
        };
    }



    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }


    public bool IsSliding()
    {
        return _isSliding;
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
