using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFShock : MonoBehaviour {

    public float timeTillDestroyed = 2f;
    float time;

    public void Start() {
        time = timeTillDestroyed;
    }

    public void Update() {
        if (time >= 0)
            time -= Time.deltaTime;
        if (time <= 0)
            Destroy(gameObject);
    }

    public float getDamage() {
        return GetComponent<AttackHitbox>().damage;
    }
    public void setDamage(float damage) {
        GetComponentInChildren<AttackHitbox>().damage = damage;
    }
}

