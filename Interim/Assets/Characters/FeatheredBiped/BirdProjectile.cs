using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdProjectile : MonoBehaviour
{
    public float maxLifetime = 10f;
    public float maxSpeed = 10f;
    public float accelerationTime = 2f;
    public float accelerationDelay = 1f;

    Vector2 target;
    Vector2 targetVel;
    float currLifetime;
    float currAccelTime;
    bool isAccel;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currLifetime = 0;
        currAccelTime = 0;
        isAccel = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currLifetime += Time.deltaTime;

        if(currLifetime > maxLifetime)
        {
            Destroy(gameObject);
        }

        if(currLifetime >= accelerationDelay)
        {
            isAccel = true;
        }

        if(isAccel)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, targetVel, currAccelTime / accelerationTime);
            currAccelTime += Time.deltaTime;

            if(currAccelTime >= accelerationTime)
            {
                isAccel=false;
            }
        }

    }

    public void init(Vector2 target)
    {
        this.target = target;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);


        targetVel = (target - pos).normalized * maxSpeed;

        targetVel = Vector2.ClampMagnitude(targetVel, maxSpeed);

    }
}
