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
        flicker.Flicker();
    }
}
