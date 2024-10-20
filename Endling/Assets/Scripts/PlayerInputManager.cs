using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour
{
    private bool isWalking;
    private bool isSprinting;
    private bool isGrounded;
    public float moveSpeed;
    public float jumpHeight;

    private Rigidbody2D playerRb;
    private PlayerAnimator playerAnimator;
    private Transform playerTransform;

    //Debug Gizmo visualiser for attach range transform REMOVE
    //public Transform rayOrigin;

    private PlayerInventory playerInventory;
    private WeaponAnimatorManager weaponAnimatorManager;
    private WeaponTypes currentWeapon;

    private Vector2 moveInput;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        weaponAnimatorManager = GetComponent<WeaponAnimatorManager>();
        playerInventory = PlayerInventory.Instance;
        Animator animator = playerRb.GetComponent<Animator>();  
        playerAnimator = new PlayerAnimator(animator);

        currentWeapon = WeaponTypes.Unarmed;

        jumpHeight = 42f;
    }

    void FixedUpdate()
    {
        Run();
        HandleSprint();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump (InputValue value) 
    {
        if (value.isPressed) 
        {
            Jump();
        }
    }

    void OnFire(InputValue value) 
    {
        if (value.isPressed) 
        {
            Attack();
        }
    }
    void OnNextWeapon(InputValue value) 
    
    {
        if (value.isPressed) 
        {
            currentWeapon++;
            if ((int)currentWeapon >= Enum.GetValues(typeof(WeaponTypes)).Length)
            {
                currentWeapon = 0;
            }
            weaponAnimatorManager.OnWeaponChangedUpdateAnimController(currentWeapon);
            playerInventory.EquipWeapon(currentWeapon);
        }
    }

    void OnPreviousWeapon(InputValue value) 
    {
        currentWeapon--;
        if ((int)currentWeapon < 0)
        {
            currentWeapon = (WeaponTypes)Enum.GetValues(typeof(WeaponTypes)).Length - 1;
        }
        weaponAnimatorManager.OnWeaponChangedUpdateAnimController(currentWeapon);
        playerInventory.EquipWeapon(currentWeapon);
    }
    private void Run() 
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRb.velocity.y);
        playerRb.velocity = playerVelocity;

        isWalking = Mathf.Abs(playerRb.velocity.x) > Mathf.Epsilon;

        if (isWalking)
        {
            playerTransform.localScale = new Vector2(Mathf.Sign(playerRb.velocity.x), 1f);
        }

        playerAnimator.UpdateRunAnimState(isWalking, isSprinting);

    }

    private void Jump() 
    {
        playerRb.velocity += new Vector2(0f, jumpHeight);
        playerAnimator.JumpAnimState();
    }
    private void HandleSprint() 
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            moveSpeed = 12f;
            isSprinting = true;
        }
        else
        {
            moveSpeed = 6f;
            isSprinting = false;
        }
    }
    private void Attack()
    {
        int weaponDamage = playerInventory.currentWeapon.CalculateDamageValue();
        float? weaponRange = playerInventory.currentWeapon.GetAttackRange();

        if (weaponRange.HasValue && currentWeapon != WeaponTypes.Bow)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, weaponRange.Value);

            if (hit.collider != null)
            {
                IDamagable damageable = hit.collider.GetComponent<IDamagable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(weaponDamage);
                }
                //playerAnimator.AttackAnimState();
            }
        }
    }


    //private void OnDrawGizmos()
    //{
    //    // Ensure you have a valid currentWeapon and rayOrigin assigned
    //    if (playerInventory.currentWeapon != null && playerInventory.currentWeapon.GetAttackRange().HasValue && rayOrigin != null)
    //    {
    //        float attackRange = playerInventory.currentWeapon.GetAttackRange().Value;

    //        // Define the direction for your ray (e.g., right in 2D or forward in 3D)
    //        Vector3 direction = rayOrigin.right; // Adjust based on how your weapon is oriented

    //        // Draw the ray as a gizmo
    //        Gizmos.color = Color.cyan; // Set the gizmo color
    //        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + direction * attackRange);
    //    }
    //}
}
