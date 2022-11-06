using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavey : MonoBehaviour
{
    public float waveSpeed = 1f;
    public float waveHeight = 1f;
    float startY;
    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(Time.time * waveSpeed) * waveHeight, transform.position.z);
    }
}
