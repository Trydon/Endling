using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour, IInitializable
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
        swordController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/PlayerSwordAnimController");
        //TODO - Set up Bow Anims
        bowController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/PlayerUnarmedAnimController");
    }

    public void Initialize(PlayerAnimator sharedPlayerAnimator) 
    {
        playerAnimator = sharedPlayerAnimator; 
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
