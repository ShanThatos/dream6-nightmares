using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarrier : MonoBehaviour
{
    public float lifetime = 5f;
    public Vector3 movement;

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }

        Vector3 pos = transform.position;
        pos += movement * Time.deltaTime;
        transform.position = pos;
    }

}
