using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour {

    public GameObject prefabToSpawn;

    public float minSpawnTime = 3f;
    public float maxSpawnTime = 7f;

    public float minAngle = 0f;
    public float maxAngle = 0f;

    public float minAngularVelocity = 0f;
    public float maxAngularVelocity = 0f;

    public float minScale = 1f;
    public float maxScale = 1f;

    float spawnTime;

    [Tooltip("Set to 0 for infinite")]
    public int maxCount;

    int count;

    Collider2D spawnTrigger;


    void Start() {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        spawnTrigger = GetComponent<Collider2D>();
        count = 0;
    }

    void Update() {
        if (spawnTrigger.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0) {
                GameObject go = Instantiate(prefabToSpawn, transform.position, Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle)));
                go.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
                if (go.GetComponent<Rigidbody2D>() != null) 
                    go.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(minAngularVelocity, maxAngularVelocity);
                spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

                count++;
                if (maxCount != 0 && count >= maxCount) 
                { 
                    Destroy(gameObject);
                }  
            }
        }
    }
}
