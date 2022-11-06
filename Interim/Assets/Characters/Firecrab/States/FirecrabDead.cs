using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabDead : FirecrabState
{
    public override void enter()
    {
        controller.animator.Play("Dead");
        controller.damagable.damageReduction = .99f;
        Debug.Log("Crab Dead");
        LeanTween.delayedCall(gameObject, 3f, () =>
        {
            PlayerPrefs.SetInt("ElioSolved", 1);
            DialogueManager.instance.PlayDialogue("DefeatCrab");
        });
    }

    public override void run()
    {
        // pass
    }

    public override string getStateName()
    {
        return "FCDead";
    }
}
