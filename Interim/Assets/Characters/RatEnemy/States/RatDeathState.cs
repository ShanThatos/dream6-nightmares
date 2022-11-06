using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatDeathState : RatEnemyState {

    public override void enter() {
        controller.animator.Play("RatDeath");

        controller.rb.velocity = Vector2.zero;
        GameObject particles = Instantiate(controller.deathParticles, transform.position, Quaternion.identity);
        particles.transform.localScale = Vector3.Scale(particles.transform.localScale, controller.transform.localScale);
    }

    public override void run() {
        if (controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) 
            Destroy(controller.gameObject);
    }

    public override string getStateName() {
        return "RatDeath";
    }
}

