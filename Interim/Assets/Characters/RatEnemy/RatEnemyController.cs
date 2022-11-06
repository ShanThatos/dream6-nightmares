using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatEnemyController : StateManager {

    public Rigidbody2D rb;

    public Animator animator;

    Dictionary<string, Transform> points = new Dictionary<string, Transform>();


    public float speed = 3f;
    public bool canJumpAttack = true;

    public GameObject deathParticles;

    Damagable damage;
    bool facingRight;

    FlickerSprite flicker;

    public override void init() {
        base.init();
        flicker = GetComponent<FlickerSprite>();
    }

    void Awake()
    {
        damage = GetComponent<Damagable>();
        damage.OnDeath += OnDead;
        damage.OnHurt += OnHurt;
    }

    public bool isFacingRight() {
        return facingRight;
    }

    public Transform getPoint(string pointName) {
        if (!points.ContainsKey(pointName))
            points.Add(pointName, transform.Find("Points").Find(pointName));
        return points[pointName];
    }

    public void setDirection(bool isFacingRight) {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (isFacingRight ? -1 : 1), transform.localScale.y, transform.localScale.z);
        facingRight = isFacingRight;
    }

    public override void run() {}

    public void OnDead() {
        switchState("RatDeath");
    }

    public void OnHurt(float damage, bool isEnergy) {
        switchState("RatHurt");
        flicker.Flicker();
    }
}
