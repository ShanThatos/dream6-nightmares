using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimer : MonoBehaviour
{

    public float despawnTime = 10f;
    float time;

    void Start() {
        time = despawnTime;
    }

    void Update() {
        if ((time -= Time.deltaTime) <= 0) 
            Destroy(gameObject);
    }
}
