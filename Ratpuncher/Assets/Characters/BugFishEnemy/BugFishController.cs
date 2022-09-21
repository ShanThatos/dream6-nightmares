using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BugFishController : StateManager {

    public Rigidbody2D rb;

    public Animator animator;

    public BugFishAnimationCues cues;

    public float maxHealth = 100;
    float currentHealth;

    public bool invincible = false;
    public Vector2 fishBaseKnockback;

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

    public float hurtTime = .5f;

    public float MIN_X = -10f;
    public float MAX_X = 10f;
    public float MAX_Y = 10f;

    Damagable damage;

    public BossHPBar HPBar;

    public override void init()
    {
        base.init();
        currentHealth = maxHealth;
    }

    public Transform getPoint(string pointName) {
        if (!points.ContainsKey(pointName))
            points.Add(pointName, transform.Find("Points").Find(pointName));
        return points[pointName];
    }

    void Awake()
    {
        TryGetComponent<Damagable>(out damage);
        if (damage)
        {
            damage.OnHurt += OnHurt;
            damage.OnDeath += OnDeath;
        }
    }

    public void setDirection(bool isFacingRight) {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (isFacingRight ? -1 : 1), transform.localScale.y, transform.localScale.z);
    }

    public override void run() {
        base.run();
        if (currentHealth < 0) {
            Destroy(gameObject);
            return;
        }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MIN_X, MAX_X), Mathf.Min(transform.position.y, MAX_Y), transform.position.z);
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (invincible)
            return;
        GameObject hit = collision.gameObject;
        /* if (hit.layer == LayerMask.NameToLayer("PlayerAttack")) {
            AttackCollider ac = hit.GetComponent<AttackCollider>();
            if (ac.canAttack())
            {
                ac.resetCooldown();
                Vector2 knockback = fishBaseKnockback * ac.attackKnockback;
                knockback.x *= (hit.transform.position.x <= transform.position.x ? 1 : 1);
                rb.AddForce(knockback, ForceMode2D.Impulse);
                currentHealth -= ac.attackDamage;

                if (currentState.getStateName() == "BFIdle" || currentState.getStateName() == "BFFlop")
                    switchState("BFHurt");
            } 
        } */
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(MIN_X, -100, 0), new Vector3(MIN_X, 100, 0));
        Gizmos.DrawLine(new Vector3(MAX_X, -100, 0), new Vector3(MAX_X, 100, 0));
        Gizmos.DrawLine(new Vector3(MIN_X, MAX_Y, 0), new Vector3(MAX_X, MAX_Y, 0));
    }

    void OnHurt(float damage)
    {
        Debug.Log("Bugfish took " + damage + " damage!!");

        if (currentState.getStateName() == "BFIdle" || currentState.getStateName() == "BFFlop")
            switchState("BFHurt");

        currentHealth -= damage;

        if (HPBar)
        {
            HPBar.RecieveDamage(damage);
        }
    }

    void OnDeath()
    {

    }

    public float getCurrentHealth() {
        return currentHealth;
    }
}

