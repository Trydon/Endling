using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializationManager : MonoBehaviour
{
    private PlayerAnimator playerAnimator;
    private PlayerInputManager playerInputManager;
    private WeaponAnimatorManager weaponAnimatorManager;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerAnimator = new PlayerAnimator(animator);

        playerInputManager = GetComponent<PlayerInputManager>();
        weaponAnimatorManager = GetComponent <WeaponAnimatorManager>();

        IInitializable[] initializables = GetComponents<IInitializable>();

        foreach (var component in initializables) 
        {
            component.Initialize(playerAnimator);
        }
    }
}
