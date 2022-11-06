using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFFlyState : BFState {


    bool isGoingRight;
    float time;

    public override void enter() {

        Collider2D bfCollider = controller.GetComponent<Collider2D>();
        bfCollider.offset = new Vector2(bfCollider.offset.x, 2f);

        controller.animator.Play("BFFly");
        time = -1;
    }

    public override void run() {
        if (time >= 0)
            time -= Time.deltaTime;
        if (time <= 0) {
            isGoingRight = UnityEngine.Random.Range(0, 2) == 0;
            time = UnityEngine.Random.Range(controller.flyFrequency.x, controller.flyFrequency.y);

            if (Mathf.Abs(controller.transform.position.x - controller.MAX_X) < 0.1f)
                isGoingRight = !isGoingRight;
            if (Mathf.Abs(controller.transform.position.x - controller.MIN_X) < 0.1f)
                isGoingRight = !isGoingRight;

            controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
            controller.rb.AddForce(new Vector2(isGoingRight ? controller.flySideForce : -controller.flySideForce, controller.flyUpForce), ForceMode2D.Impulse);
            controller.setDirection(isGoingRight);
        }
    }

    public override string getStateName() {
        return "BFFly";
    }
}

