using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class MothEnemyController : StateManager {

    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public Animator animator;

    FlickerSprite flicker;


    public UnityEvent onDeathEvent;

    [HideInInspector]
    public MothEnemyAnimationCues cues;


    public override void init() {
        base.init();
        flicker = GetComponent<FlickerSprite>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        cues = GetComponentInChildren<MothEnemyAnimationCues>();
    }



    Dictionary<string, Transform> points = new Dictionary<string, Transform>();
    public Transform getPoint(string pointName) {
        if (!points.ContainsKey(pointName))
            points.Add(pointName, transform.Find("Points").Find(pointName));
        return points[pointName];
    }



    bool facingRight;
    public void setDirection(bool isFacingRight) {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (isFacingRight ? -1 : 1), transform.localScale.y, transform.localScale.z);
        facingRight = isFacingRight;
    }





    public override void run() {}





    Damagable damage;

    void Awake()
    {
        damage = GetComponent<Damagable>();
        damage.OnDeath += OnDead;
        damage.OnHurt += OnHurt;
    }

    public void OnDead() {
        switchState("MothDeath");
    }

    public void OnHurt(float damage) {
        switchState("MothRun");
        flicker.Flicker();
    }

    public Damagable getDamagable() {
        return damage;
    }
}
