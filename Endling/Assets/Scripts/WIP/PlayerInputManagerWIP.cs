using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManagerWIP : MonoBehaviour
{

    private Rigidbody2D playerRb;
    private Transform playerTransform;
    private Animator playerAnimator;

    private PlayerMovementControllerWIP playerMovementController;
    private PlayerAnimationControllerWIP playerAnimationController;

    private Vector2 moveInput;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerMovementController = new PlayerMovementControllerWIP(playerRb, playerTransform);

        playerAnimator = GetComponent<Animator>();
        playerAnimationController = new PlayerAnimationControllerWIP(playerAnimator);


    }

    private void FixedUpdate()
    {
        playerMovementController.HandleMovement(moveInput);
    }

    private void Update()
    {
        OnSprint();
        HandleMovementAnimation();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && playerMovementController.jumpTimerSeconds <= 0)
        { 
            playerMovementController.jumpTimerSeconds = PlayerMovementControllerWIP.JumpDelaySeconds;
        }
    }

    private void OnSprint() 
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            playerMovementController.StartSprinting();
        }
        else
        {
            playerMovementController.StopSprinting();

        }
    }

    private void HandleMovementAnimation() 
    {
        playerAnimationController.UpdateRunAnimState(playerMovementController.IsWalking, playerMovementController.IsSprinting);

        if (!playerMovementController.IsGrounded && playerMovementController.jumpTimerSeconds > 0)
        {
            playerAnimationController.JumpAnimState();
        }
    }
} 

