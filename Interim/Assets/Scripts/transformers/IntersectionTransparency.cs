using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class IntersectionTransparency : MonoBehaviour {
    SpriteRenderer sr;
    List<Collider2D> colliders;
    Collider2D targetCollider;

    public float transitionTime = 0.5f;
    public float minAlpha = 0.7f;

    float currentTime = 0f;

    void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();
        colliders = new List<Collider2D>();
        GetComponentInChildren<Rigidbody2D>().GetAttachedColliders(colliders);
        targetCollider = GameManager.instance.player.GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        bool isColliding = colliders.Exists(c => c.bounds.Intersects(targetCollider.bounds));
        float newCurrentTime = Mathf.Clamp(currentTime + Time.deltaTime / transitionTime * (isColliding ? 1 : -1), 0, 1);
        if (newCurrentTime != currentTime) {
            currentTime = newCurrentTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Max(minAlpha, Smoothing.EaseInOut(1 - currentTime)));
        }
    }
}
