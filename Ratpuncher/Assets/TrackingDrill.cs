using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingDrill : MonoBehaviour
{

    [Tooltip("How long before launching attack")]
    public float delay;

    [Tooltip("Tracking speed")]
    public float speed;

    [Tooltip("GameObject to spawn on attack")]
    public GameObject attack;

    Transform player;
    Rigidbody2D rb;
    bool shouldTrack = false;
    bool hasAttacked = false;
    float lifetime = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAttacked)
        {
            return;
        }

        if (shouldTrack)
        {
            if(player.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }
        }

        if(Mathf.Abs(player.position.x - transform.position.x) < 0.001)
        {
            shouldTrack = false;
        }
        else
        {
            shouldTrack = true;
        }

        lifetime += Time.deltaTime;
        if(lifetime >= delay)
        {
            executeAttck();
        }
    }

    private void executeAttck()
    {
        attack.SetActive(true);
        ParticleSystem temp = GetComponent<ParticleSystem>();
        temp.enableEmission = false;
        rb.velocity = Vector2.zero;
        hasAttacked = true;

        Destroy(this, 3f);
    }
}
