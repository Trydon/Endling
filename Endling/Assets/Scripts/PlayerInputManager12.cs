using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager2 : MonoBehaviour
{

    private Rigidbody2D playerRb;
    private Transform playerTransform;

    private PlayerMovementController playerMovementController;

    private Vector2 moveInput;



    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerMovementController = new PlayerMovementController(playerRb, playerTransform);
    }

    private void FixedUpdate()
    {
        playerMovementController.HandleMovement(moveInput);
    }

    private void Update()
    {
        OnSprint();
    }


    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && playerMovementController.jumpTimerSeconds <= 0)
        { 
            playerMovementController.jumpTimerSeconds = PlayerMovementController.JumpDelaySeconds;
        }
    }

    private void OnSprint() 
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            playerMovementController._isSprinting = true;
        }
        else
        {
            playerMovementController._isSprinting = false;
        }

        playerMovementController.HandleSprint();
    }
} 

