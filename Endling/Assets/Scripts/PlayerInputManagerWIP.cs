using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManagerWIP : MonoBehaviour
{

    private Rigidbody2D _playerRb;
    private Transform _playerTransform;
    private Animator _playerAnimator;

    private PlayerMovementControllerWIP playerMovementController;
    private PlayerAnimationControllerWIP playerAnimationController;
    private PlayerInventoryController playerInventoryController;


    private Vector2 moveInput;


    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        playerAnimationController = new PlayerAnimationControllerWIP(_playerAnimator);

        _playerRb = GetComponent<Rigidbody2D>();
        _playerTransform = GetComponent<Transform>();
        playerMovementController = new PlayerMovementControllerWIP(_playerRb, _playerTransform);
        playerInventoryController = new PlayerInventoryController();

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

    private void OnJump(InputValue value)
    {
        if (value.isPressed && playerMovementController.jumpTimerSeconds <= 0)
        { 
            playerMovementController.jumpTimerSeconds = PlayerMovementControllerWIP.JumpDelaySeconds;
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            playerAnimationController.AttackAnimState();
            //Attack();
        }
    }
    void OnNextWeapon(InputValue value) 
    {
            WeaponTypes nextWeapon = playerInventoryController.NextWeapon();
            playerAnimationController.OnWeaponChangedUpdateAnimController(nextWeapon);
    }

    void OnPreviousWeapon(InputValue value) 
    {
        WeaponTypes previousWeapon = playerInventoryController.PrevWeapon();
        playerAnimationController.OnWeaponChangedUpdateAnimController(previousWeapon);
    }


    private void HandleMovementAnimation() 
    {
        playerAnimationController.UpdateRunAnimState(playerMovementController.IsWalking, playerMovementController.IsSprinting);
        playerAnimationController.JumpAnimState(playerMovementController.IsGrounded, playerMovementController.jumpTimerSeconds);        
    }
} 

