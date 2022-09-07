using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoonController : StateManager {

    public ChaseState chaseState;

    protected override State checkForAnyState() {
        if (chaseState.shouldChasePlayer(currentState.getStateName()))
            return chaseState;
        return null;
    }


    private Rigidbody2D rb;

    public override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Update() {
        base.Update();
        if (rb.velocity.x > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (rb.velocity.x < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}