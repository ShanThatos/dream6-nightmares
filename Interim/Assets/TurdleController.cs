using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurdleController : MonoBehaviour
{
    public Transform volcanoPoint;

    public GameObject volcanoEmitter;

    public float minDelay;
    public float maxDelay;

    float delay;
    float timer;

    private void Start()
    {
        delay = Random.Range(minDelay, maxDelay);
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= delay)
        {
            Instantiate(volcanoEmitter, volcanoPoint.transform.position, Quaternion.identity);
            delay = Random.Range(minDelay, maxDelay);
            timer = 0;
        }
    }
}

