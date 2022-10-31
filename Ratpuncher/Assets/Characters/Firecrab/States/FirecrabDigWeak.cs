using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabDigWeak : FirecrabState
{
    public float duration = 3.33f;

    float prevDR;
    float timer;

    public override void enter()
    {
        timer = duration;
        controller.animator.Play("DigWeak");
        prevDR = controller.damagable.damageReduction;
        controller.damagable.damageReduction = 0;
    }

    public override void run()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            controller.switchState("FCIdle");
        }
    }

    public override void exit()
    {
        controller.damagable.damageReduction = prevDR;
    }

    public override string getStateName()
    {
        return "FCDigWeak";
    }
}
