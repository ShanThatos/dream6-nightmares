using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class MothState : State {

    protected MothEnemyController controller;

    public override void init() {
        controller = (MothEnemyController) manager;
    }

    public float getDistanceToPlayer() {
        return Vector2.Distance(transform.position, GameManager.GetPlayerTransform().position);
    }

    public virtual void reset() {}
}

