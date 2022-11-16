using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabWeak : FirecrabState
{
    public float duration = 4f;

    float prevDR;
    float timer;
    bool isStandingUp;

    public override void enter()
    {
        timer = duration;
        controller.animator.SetTrigger("Weak");
        prevDR = controller.damagable.damageReduction;
        controller.damagable.damageReduction = 0.5f;
        controller.damagable.energyDamageReduction = 0f;
        isStandingUp = false;
    }

    public override void run()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            if (isStandingUp)
            {
                controller.switchState("FCIdle");
                return;
            }
            else
            {
                controller.animator.SetTrigger("Stand");
                controller.damagable.damageReduction = prevDR;
                timer = 1.05f;
                isStandingUp = true;
            }
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
