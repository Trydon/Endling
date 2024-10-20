using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    public static WeaponAnimatorManager Instance { get; private set; }
    private PlayerAnimator playerAnimator;

    private RuntimeAnimatorController unarmedController;
    private RuntimeAnimatorController swordController;
    private RuntimeAnimatorController bowController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        unarmedController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/PlayerUnarmedAnimController");
        swordController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/PlayerUnarmedAnimController");
        bowController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/PlayerUnarmedAnimController");

        // Initialize the PlayerAnimator with the existing animator component (starts as unarmed)
        Animator animator = GetComponent<Animator>();
        playerAnimator = new PlayerAnimator(animator);
    }

    // this will be called in the input manager when weapon is changed
    public void OnWeaponChangedUpdateAnimController(WeaponTypes weaponTypes) 
    {
        RuntimeAnimatorController newController = GetWeaponAnimController(weaponTypes);
        Debug.Log("Weapon Type = " + weaponTypes);
        playerAnimator.ChangeAnimatorController(newController);
    }

    private RuntimeAnimatorController GetWeaponAnimController(WeaponTypes weaponType) 
    {
        switch (weaponType) 
        {
            case WeaponTypes.Unarmed:
                return unarmedController;
            case WeaponTypes.Sword:
                return swordController;
            case WeaponTypes.Bow:
                return bowController;
            default:
                return null;
        }
    }

}
