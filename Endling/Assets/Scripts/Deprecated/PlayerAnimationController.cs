using UnityEngine;

public class PlayerAnimationController
{
    private Animator animator;

    public PlayerAnimationController(Animator animator)
    {
        this.animator = animator;
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
