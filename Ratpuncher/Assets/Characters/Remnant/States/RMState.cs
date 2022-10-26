using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class RMState : State {

    protected RemnantController controller;

    public override void init() {
        controller = (RemnantController) manager;
    }

    public float getDistanceToPlayer() {
        return Vector2.Distance(transform.position, GameManager.GetPlayerTransform().position);
    }

    public virtual void reset() {}
}

