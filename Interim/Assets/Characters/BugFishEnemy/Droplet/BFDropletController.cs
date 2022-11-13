using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFDropletController : MonoBehaviour {
    
    public GameObject shockSpawnerPrefab;
    public float damage;

    Collider2D col;

    void Start() {
        col = GetComponent<Collider2D>();
    }

    void Update() {
        if (col.IsTouchingLayers(LayerMask.GetMask("Platform"))) {
            GameObject shockspawner = Instantiate(shockSpawnerPrefab, transform.position, Quaternion.identity);
            shockspawner.GetComponent<BFShockSpawner>().setDamage(damage);
            Destroy(gameObject);
        }
    }
}
