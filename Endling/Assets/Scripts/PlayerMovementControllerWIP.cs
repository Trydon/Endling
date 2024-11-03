using UnityEngine;

public class PlayerMovementControllerWIP
{

    private bool _isWalking;
    private bool _isSprinting;
    private bool _isGrounded;

    public bool IsWalking
    {
        get { return _isWalking; }
    }
    public bool IsSprinting
    {
        get { return _isSprinting; }
    }

    public bool IsGrounded
    {
        get { return _isGrounded; }
    }


    public float MoveSpeed { get; private set; }
    public float JumpHeight { get; private set; } = 42f;

    private static readonly float _JumpDelaySeconds = 0.2f;
    public static float JumpDelaySeconds 
    {  
        get { return _JumpDelaySeconds; } 
    }
    public float jumpTimerSeconds;

    private const int GroundLayerIndex = 7;
    private const float PlayerGroundedCastLength = 1.1f;

    private Rigidbody2D _playerRb;
    private Transform _playerTransform;

    private LayerMask _groundMask;
    private Vector2 _movementInput;
    private readonly Vector3 _colliderOffset = new Vector3(0.435f, 0, 0);

    public PlayerMovementControllerWIP(Rigidbody2D playerRb, Transform playerTransform)
    {
        this._playerRb = playerRb;
        this._playerTransform = playerTransform;
        _groundMask = 1 << GroundLayerIndex;
    }


    public void HandleMovement(Vector2 moveInput)
    {
        Run(moveInput);
        HandleJump();
        GroundCheck();
    }


    private void Run(Vector2 moveInput)
    {
        _movementInput = moveInput;
        Vector2 playerVelocity = new Vector2(_movementInput.x * MoveSpeed, _playerRb.velocity.y);
        _playerRb.velocity = playerVelocity;

        _isWalking = Mathf.Abs(_playerRb.velocity.x) > Mathf.Epsilon;

        if (_isWalking)
        {
            _playerTransform.localScale = new Vector2(Mathf.Sign(_playerRb.velocity.x), 1f);
        }
    }

    private void Jump()
    {
        _playerRb.velocity += new Vector2(0f, JumpHeight);
        jumpTimerSeconds = 0;
    }

    private void HandleJump()
    {
        if (jumpTimerSeconds > 0 && _isGrounded)
        {
            Jump();
            jumpTimerSeconds = 0;
        }

        if (jumpTimerSeconds > 0)
        {
            jumpTimerSeconds -= Time.deltaTime;
        }
    }

    public void StartSprinting() 
    {
        _isSprinting = true;
        HandleSprint();
    }

    public void StopSprinting() 
    {
        _isSprinting = false;
        HandleSprint();
    }

    public void HandleSprint()
    {
        MoveSpeed = _isSprinting ? 12f : 6f;
    }

    private bool GroundCheck() 
    {
        _isGrounded = Physics2D.Raycast(_playerTransform.position + _colliderOffset, -_playerTransform.up, PlayerGroundedCastLength, _groundMask) || Physics2D.Raycast(_playerTransform.position - _colliderOffset, -_playerTransform.up, PlayerGroundedCastLength, _groundMask);
        return _isGrounded;
    }
}


