using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NarwhaleController : StateManager
{
    
    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public FlickerSprite flicker;

    public UnityEvent onDeathEvent;

    [HideInInspector]
    public NarwhaleAnimationCues cues;

    [HideInInspector]
    public BoundedTransform bounds;

    public override void init() {
        base.init();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        flicker = GetComponent<FlickerSprite>();
        cues = GetComponentInChildren<NarwhaleAnimationCues>();
        bounds = GetComponent<BoundedTransform>();
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
    public bool isFacingRight() {
        return facingRight;
    }

    public bool isAnimationDone(string animationName) {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    public override void run() {}

    Damagable damage;

    private void Awake() {
        damage = GetComponent<Damagable>();
        damage.OnDeath += OnDead;
        damage.OnHurt += OnHurt;
    }

    public void OnDead() {
        switchState("NWDeath");
    }

    public void OnHurt(float damage, bool isEnegy) {
        flicker.Flicker();
    }

    public Damagable GetDamagable() {
        return damage;
    }
}
