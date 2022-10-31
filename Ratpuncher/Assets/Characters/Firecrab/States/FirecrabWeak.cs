using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabWeak : FirecrabState
{
    public float duration = 4f;

    float prevDR;
    float timer;
    public override void enter()
    {
        timer = duration;
        controller.animator.Play("Weak");
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
        return "FCWeak";
    }
}
