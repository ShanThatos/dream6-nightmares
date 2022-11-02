using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{
    public float minInterval;
    public float maxInterval;
    private float timer;


    private Animator ani;

    private void Start()
    {
        timer = Random.Range(minInterval, maxInterval);
        ani = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            ani.SetTrigger("Snap");
            timer = 1000.0f;
        }
    }

    public void SnapDone()
    {
        timer = Random.Range(minInterval, maxInterval);
    }
}
