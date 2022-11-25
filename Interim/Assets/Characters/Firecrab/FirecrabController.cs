using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirecrabController : StateManager
{
    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public FlickerSprite flicker;

    [HideInInspector]
    public Damagable damagable;

    [HideInInspector]
    public float tempRangeMult = 1;

    private int stunCounter = 3;
    private bool canStun;

    // Start is called before the first frame update
    public override void init()
    {
        base.init();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        flicker = GetComponent<FlickerSprite>();
        damagable = GetComponent<Damagable>();

        damagable.OnDeath += OnDeath;
        damagable.OnHurt += OnHurt;
    }

    public bool isAnimationDone(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    void OnDeath()
    {
        switchState("FCDead");
    }

    void OnHurt(float damage, bool isEnergy)
    {
        if(currentState.getStateName() == "FCWeak" || currentState.getStateName() == "FCDigWeak")
        {
            flicker.Flicker();
        }

        if (canStun)
        {
            int hits = isEnergy ? 3 : 1;
            stunCounter -= hits;

            if(stunCounter <= 0)
            {
                stunCounter = 3;
                canStun = false;
                switchState("FCWeak");
            }
        }
    }

    public void allowStun(bool stun = true)
    {
        stunCounter = 3;
        canStun = stun;
    }

}
