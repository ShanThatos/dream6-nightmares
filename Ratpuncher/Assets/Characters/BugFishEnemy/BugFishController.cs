using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class BugFishController : StateManager {

    public Rigidbody2D rb;
    public bool isAnimating;

    public Animator animator;

    public BugFishAnimationCues cues;

    Dictionary<string, Transform> points = new Dictionary<string, Transform>();


    public float idleSpeed = 3f;
    public float flyUpForce = 4f;
    public float flySideForce = 4f;
    public Vector2 flyFrequency = new Vector2(.3f, 1f);
    public float flopUpForce = 50f;
    public float flopUpSideForce = 10f;
    public float flopDownForce = 10f;
    public GameObject shockSpawnerPrefab;
    public GameObject groundPoundPrefab;

    public float MIN_DROP_COOLDOWN = 4f;
    public float MAX_DROP_COOLDOWN = 8f;
    public float dripWaitTime = 1f;
    public GameObject dropletPrefab;

    public float hurtTime = .5f;

    public float MIN_X = -10f;
    public float MAX_X = 10f;
    public float MAX_Y = 10f;

    Damagable damage;

    public BossHPBar HPBar;

    public UnityEvent onDeathEvent;

    FlickerSprite flicker;

    public Transform getPoint(string pointName) {
        if (!points.ContainsKey(pointName))
            points.Add(pointName, transform.Find("Points").Find(pointName));
        return points[pointName];
    }

    public override void init() {
        base.init();

        flicker = GetComponent<FlickerSprite>();
        TryGetComponent<Damagable>(out damage);
        if (damage) {
            damage.OnHurt += OnHurt;
            damage.OnDeath += OnDeath;
        }

        HPBar.SetMaxHP(damage.GetMaxHealth());

        setAnimating(this.isAnimating);
    }


    public void setDirection(bool isFacingRight) {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (isFacingRight ? -1 : 1), transform.localScale.y, transform.localScale.z);
    }

    public override void run() {
        if (isAnimating) return;
        base.run();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_X, MAX_X), Mathf.Min(transform.position.y, MAX_Y), transform.position.z);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(MIN_X, -100, 0), new Vector3(MIN_X, 100, 0));
        Gizmos.DrawLine(new Vector3(MAX_X, -100, 0), new Vector3(MAX_X, 100, 0));
        Gizmos.DrawLine(new Vector3(MIN_X, MAX_Y, 0), new Vector3(MAX_X, MAX_Y, 0));
    }

    void OnHurt(float damage)
    {
        // Debug.Log("Bugfish took " + damage + " damage!!");

        if (currentState.getStateName() == "BFIdle")
            switchState("BFHurt");

        if (HPBar) 
            HPBar.RecieveDamage(damage);
        if (flicker) 
            flicker.Flicker();
    }

    void OnDeath() {
        if (HPBar)
            HPBar.OnDeath();
        switchState("BFDeath");
    }

    public Damagable getDamagable() {
        return damage;
    }

    private void OnDestroy() {}


    public void setAnimating(bool isAnimating) {
        this.isAnimating = isAnimating;
        if (isAnimating) {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        } else {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

