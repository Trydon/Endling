using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializationManager : MonoBehaviour
{
    private PlayerAnimationController playerAnimator;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerAnimator = new PlayerAnimationController(animator);

        IInitializable[] initializables = GetComponents<IInitializable>();

        foreach (var component in initializables) 
        {
            component.Initialize(playerAnimator);
        }
    }
}
