using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState : State {

    public Animator animator;
    public ChaseState chaseState;


    public override void onEnter() {
        // animator.Play("Rat_Attack");
        animator.SetTrigger("Attack");
        // Debug.Log(animator.playbackTime);
    }

    public override State run() {
        return chaseState;
    }

    public override string getStateName() { return "Attack"; }
}