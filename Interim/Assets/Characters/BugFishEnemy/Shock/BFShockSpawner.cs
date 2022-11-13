using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFShockSpawner : MonoBehaviour {

    public GameObject shockPrefab;

    public float timeBetweenShocks;
    public float distanceBetweenShocks;
    public float shockStartX;
    public float totalShocks;

    float time;
    float currentX = 0;
    int currentShock = 0;

    float damage;

    public void Start() {
        time = timeBetweenShocks;
        currentX = shockStartX;
    }


    public void FixedUpdate() {
        if (time >= 0)
            time -= Time.deltaTime;
        
        if (time <= 0 && currentShock < totalShocks) {
            GameObject shock = Instantiate(shockPrefab, transform.position + Vector3.right * currentX, Quaternion.identity);
            GameObject shock2 = Instantiate(shockPrefab, transform.position + Vector3.left * currentX, Quaternion.identity);
            time = timeBetweenShocks;
            currentX += distanceBetweenShocks;
            currentShock++;

            shock.GetComponent<BFShock>().setDamage(damage);
            shock2.GetComponent<BFShock>().setDamage(damage);
        }

        if (currentShock >= totalShocks)
            Destroy(gameObject);
    }

    public float getDamage() {
        return damage;
    }
    public void setDamage(float damage) {
        this.damage = damage;
    }
}

