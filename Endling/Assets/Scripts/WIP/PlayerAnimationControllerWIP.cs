using UnityEngine;

public class PlayerAnimationControllerWIP
{
    private Animator animator;
    private RuntimeAnimatorController unarmedController;
    private RuntimeAnimatorController swordController;
    private RuntimeAnimatorController bowController;

    public PlayerAnimationControllerWIP(Animator animator)
    {
        this.animator = animator;
    }

    public void LoadAnimatorControllers() 
    {
        unarmedController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/PlayerUnarmedAnimController");
        swordController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/PlayerSwordAnimController");
        //TODO - Set up Bow Anims
        bowController = Resources.Load<RuntimeAnimatorController>("Animations/Animators/PlayerUnarmedAnimController");
    }

    public void OnWeaponChangedUpdateAnimController(WeaponTypes weaponTypes)
    {
        RuntimeAnimatorController newController = GetWeaponAnimController(weaponTypes);
        Debug.Log("Weapon Type = " + weaponTypes);
        ChangeAnimatorController(newController);
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

    public void ChangeAnimatorController(RuntimeAnimatorController newController)
    {
        animator.runtimeAnimatorController = newController;
    }

    public void UpdateRunAnimState(bool isMoving, bool isSprinting)
    {
        animator.SetBool("IsWalking", isMoving);
        animator.SetBool("IsRunning", isSprinting);
    }

    public void JumpAnimState()
    {
        animator.SetTrigger("JumpPressed");
    }

    public void AttackAnimState()
    {
        Debug.Log(animator.ToString());
        animator.SetTrigger("AttackPressed");
    }
}