using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFDripState : BFState {

    float dripTimer = 0f;
    float dripWaitTimer;


    Vector2 previousVelocity;

    public override void enter() {
        controller.animator.Play("BFFly");
        previousVelocity = controller.rb.velocity;
        controller.rb.constraints = controller.rb.constraints | RigidbodyConstraints2D.FreezePosition;
        dripWaitTimer = controller.dripWaitTime;
    }

    public override void exit() {
        controller.rb.constraints = controller.rb.constraints & ~RigidbodyConstraints2D.FreezePosition;
        controller.rb.velocity = previousVelocity;
        dripTimer = UnityEngine.Random.Range(controller.MIN_DROP_COOLDOWN, controller.MAX_DROP_COOLDOWN);
        GameObject droplet = Instantiate(controller.dropletPrefab, controller.transform.position, Quaternion.identity);
        droplet.GetComponent<BFDropletController>().shockSpawnerPrefab = controller.shockSpawnerPrefab;
    }

    public override void run() {
        if (dripWaitTimer > 0) {
            dripWaitTimer -= Time.deltaTime;
            return;
        }
        controller.switchState("BFFly");
    }

    public void FixedUpdate() {
        if (dripTimer > 0)
            dripTimer -= Time.deltaTime;
    }

    public override bool canEnter() {
        return controller.currentState.getStateName() == "BFFly" && dripTimer <= 0;
    }

    public override string getStateName() {
        return "BFDrip";
    }
}

