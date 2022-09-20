using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RatEnemyController : StateManager {

    public Rigidbody2D rb;

    public Animator animator;

    public const float MAX_HEALTH = 30;
    public float currentHealth;
    bool isDead;

    public Vector2 ratBaseKnockback;

    Dictionary<string, Transform> points = new Dictionary<string, Transform>();


    public float speed = 3f;
    public bool canJumpAttack = true;

    public GameObject deathParticles;

    Damagable damage;
    bool facingRight;

    FlickerSprite flicker;

    public override void init() {
        base.init();
        currentHealth = MAX_HEALTH;
        flicker = GetComponent<FlickerSprite>();
    }

    void Awake()
    {
        damage = GetComponent<Damagable>();
        damage.OnDeath += OnDead;
        damage.OnHurt += OnHurt;
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

    public override void run() {
        if (currentHealth < 0 && !isDead) {
            isDead = true;
            Destroy(gameObject);
        }
    }

    public void OnDead()
    {
        if (deathParticles)
        {
            GameObject particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
            particles.transform.localScale = Vector3.Scale(particles.transform.localScale, transform.localScale);

            if (facingRight)
            {
                particles.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
            }
        }
        Destroy(gameObject);
    }

    public void OnHurt(float damage)
    {
        Debug.Log("Took " + damage + "damage!!");
        switchState("RatHurt");
        flicker.Flicker();
    }
}

