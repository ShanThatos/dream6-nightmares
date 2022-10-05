using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothIdleState : MothState {
    

    public float FLAP_UP_MAX_FORCE = 10f;
    public float FLAP_UP_MIN_FORCE = 10f;
    public float FLAP_SIDE_FORCE = 5f;

    public float X_VELOCITY_RESET_TIME = 5.0f;
    float xVelResetTime;

    public float Y_VELOCITY_RESET_TIME = 5.0f;
    float yVelResetTime;

    bool flapped;


    public override void enter() {
        controller.animator.Play("MothIdle");
        xVelResetTime = 0;
        yVelResetTime = 0;
        flapped = false;
    }

    public override void run() {
        xVelResetTime -= Time.deltaTime;
        if (xVelResetTime <= 0) {
            controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
            xVelResetTime = X_VELOCITY_RESET_TIME;
        }
        yVelResetTime -= Time.deltaTime;
        if (yVelResetTime <= 0) {
            controller.rb.velocity = new Vector2(controller.rb.velocity.x, 0);
            yVelResetTime = Y_VELOCITY_RESET_TIME;
        }

        if (controller.cues.canFlap && !flapped) {
            flapped = true;
            controller.rb.AddForce(new Vector2(Random.Range(-FLAP_SIDE_FORCE, FLAP_SIDE_FORCE), Random.Range(FLAP_UP_MIN_FORCE, FLAP_UP_MAX_FORCE)), ForceMode2D.Impulse);

            if (Mathf.Abs(controller.rb.velocity.x) > 0.5f)
                controller.setDirection(controller.rb.velocity.x > 0);
        }
        if (!controller.cues.canFlap)
            flapped = false;
    }

    public override string getStateName() {
        return "MothIdle";
    }
}

