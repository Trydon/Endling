using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour, IInitializable
{
    private bool isWalking;
    private bool isSprinting;
    private bool grounded;
    public float moveSpeed;
    public float jumpHeight;
    private float groundCasterLength;
    private float jumpBuffer = 0.2f;
    private float jumpTimer;
    private int groundLayerIndeces = 7;

    private Rigidbody2D playerRb;
    private Transform playerTransform;

    private PlayerAnimationController playerAnimator;
    private PlayerInventory playerInventory;
    private WeaponAnimatorManager weaponAnimatorManager;
   
    private WeaponTypes currentWeapon;

    private LayerMask groundMask;
    private Vector2 moveInput;
    private Vector3 colliderOffset;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        weaponAnimatorManager = GetComponent<WeaponAnimatorManager>();
        playerInventory = PlayerInventory.Instance;
        groundMask = 1 << groundLayerIndeces;
        playerInventory.EquipWeapon(currentWeapon);
        colliderOffset = new Vector3(0.435f, 0, 0);
        groundCasterLength = 1.1f;
        jumpHeight = 42f;
    }

    public void Initialize(PlayerAnimationController sharedPlayerAnimator)
    {
        playerAnimator = sharedPlayerAnimator;
    }

    void FixedUpdate()
    {
        Run();
        HandleSprint();
        HandleJump();
        grounded = Physics2D.Raycast(transform.position + colliderOffset, -transform.up, groundCasterLength, groundMask) || Physics2D.Raycast(transform.position - colliderOffset, -transform.up, groundCasterLength, groundMask);
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump (InputValue value) 
    {
        if (value.isPressed) 
        {
            jumpTimer = jumpBuffer;
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
        jumpTimer = 0;
    }

    private void HandleJump() 
    {
        if (jumpTimer > 0 && grounded) 
        {
            Jump();
            jumpTimer = 0;
        }

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
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
        playerAnimator.AttackAnimState();

        // checks that weapon range isn't null (it will be null when firing a bow)
        if (weaponRange.HasValue)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, weaponRange.Value);

            if (hit.collider != null)
            {
                IDamagable damageable = hit.collider.GetComponent<IDamagable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(weaponDamage);
                }
            }
        }
        else 
        {
            // projective weapon logic here
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

    ////        // Draw the ray as a gizmo
    //        Gizmos.color = Color.cyan; // Set the gizmo color
    //        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + direction * attackRange);
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    if (grounded)
    //    {
    //        Gizmos.color = Color.cyan;
    //    }
    //    else 
    //    {
    //        Gizmos.color = Color.green;
    //    }

    //    Gizmos.DrawLine(transform.position + colliderOffset, playerTransform.position + colliderOffset + Vector3.down * lineLength);
    //    Gizmos.DrawLine(transform.position - colliderOffset, playerTransform.position - colliderOffset + Vector3.down * lineLength);
    //}
}
