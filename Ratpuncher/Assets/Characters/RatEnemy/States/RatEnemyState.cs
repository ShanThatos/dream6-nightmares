using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class RatEnemyState : State {

    protected RatEnemyController controller;

    public override void init() {
        controller = (RatEnemyController) manager;
    }

    public bool isGrounded() {
        return Physics2D.Linecast(controller.transform.position, controller.getPoint("BottomPoint").position, LayerMask.GetMask("Platform")).collider != null;
    }
    public bool canMoveForward() {
        return isGrounded()
            && Physics2D.Linecast(controller.transform.position, controller.getPoint("ForwardPoint").position, LayerMask.GetMask("Platform")).collider == null
            && Physics2D.Linecast(controller.transform.position, controller.getPoint("ForwardBottomPoint").position, LayerMask.GetMask("Platform")).collider != null;
    }

    public float getDistanceToPlayer() {
        return Vector2.Distance(transform.position, GameManager.getPlayerTransform().position);
    }
}

