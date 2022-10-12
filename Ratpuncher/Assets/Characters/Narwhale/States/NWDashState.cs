using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NWDashState : NWState {

    const float DASH_COOLDOWN_TIME = 1.5f;
    float dashCooldownTime = 0;

    public float dashForce;
    bool finishedDashing = false;

    public override void enter() {
        controller.animator.Play("NarwhaleAttack");

        controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);

        if (controller.bounds.isNearBounds()) {
            float mid = (controller.bounds.MIN_X + controller.bounds.MAX_X) / 2;
            controller.setDirection(transform.position.x <= mid);
        } else if (Vector2.Distance(GameManager.GetPlayerTransform().position, transform.position) < 10)
            controller.setDirection(transform.position.x <= GameManager.GetPlayerTransform().position.x);
        else
            controller.setDirection(Random.value > 0.5f);

        finishedDashing = false;
    }

    public override void exit() {
        dashCooldownTime = DASH_COOLDOWN_TIME + Random.Range(-1f,1f);
    }

    public override void run() {
        if (controller.cues.canDashForward) {
            controller.rb.AddForce((controller.isFacingRight() ? Vector2.right : Vector2.left) * dashForce, ForceMode2D.Impulse);
            // controller.rb.velocity = new Vector2(controller.isFacingRight() ? dashSpeed : -dashSpeed, controller.rb.velocity.y);

            finishedDashing = true;
            return;
        }
        
        if (finishedDashing) {
            controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
            finishedDashing = false;
        }

        if (controller.isAnimationDone("NarwhaleAttack")) {
            controller.switchState("NWWalk");
        }
    }

    private void FixedUpdate() {
        if (dashCooldownTime >= 0)
            dashCooldownTime -= Time.deltaTime;
    }

    public override bool canEnter() {
        return dashCooldownTime <= 0 && controller.currentState.getStateName() == "NWWalk";
    }

    public override string getStateName() {
        return "NWDash";
    }
}
