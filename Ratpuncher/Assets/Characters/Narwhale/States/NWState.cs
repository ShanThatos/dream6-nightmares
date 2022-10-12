using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NWState : State {
    
    protected NarwhaleController controller;

    public override void init() {
        controller = (NarwhaleController) manager;
    }

    public float getDistanceToPlayer() {
        return Vector2.Distance(transform.position, GameManager.GetPlayerTransform().position);
    }

    public virtual void reset() {}
}
