using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRunning(bool isRunning)
    {
        animator.SetBool("Run", isRunning);
    }

    public void setDashing(bool isDashing)
    {
        animator.SetBool("Dash", isDashing);
    }

    public void setVertical(Enums.VerticalState state)
    {
        switch (state)
        {
            
            case Enums.VerticalState.Jumping:
                animator.SetInteger("Jump", 1);
                break;
            case Enums.VerticalState.Falling:
                animator.SetInteger("Jump", -1);
                break;
            case Enums.VerticalState.Pounding:
                animator.SetBool("Pound", true);
                break;
            default:
                animator.SetInteger("Jump", 0);
                animator.SetBool("Pound", false);
                break;
        }
    }

    public void setAttacking(bool attacking)
    {
        animator.SetBool("Attack", attacking);
    }

    public void setNextAttack()
    {
        animator.SetTrigger("NextAttack");
    }

    public void setAnimationCancel()
    {
        animator.ResetTrigger("NextAttack");
        animator.SetTrigger("AttackCancel");
    }
}
