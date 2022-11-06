using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabState : State
{
    protected FirecrabController controller;

    public override void init()
    {
        base.init();
        controller = (FirecrabController) manager;
    }

    public float getDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, GameManager.GetPlayerTransform().position);
    }

    public override string getStateName()
    {
        return "FCERROR";
    }
}
