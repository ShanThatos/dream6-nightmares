using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFDropletController : MonoBehaviour {
    
    public GameObject shockSpawnerPrefab;

    Collider2D col;

    void Start() {
        col = GetComponent<Collider2D>();
    }

    void Update() {
        if (col.IsTouchingLayers(LayerMask.GetMask("Platform"))) {
            Instantiate(shockSpawnerPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
