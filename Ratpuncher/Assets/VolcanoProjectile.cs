using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoProjectile : MonoBehaviour
{

    [Tooltip("Damagable component of projectile")]
    public Damagable dmg;

    [Tooltip("Particles to play on impact")]
    public GameObject impactParticles;


    // Start is called before the first frame update
    void Start()
    {
        if (dmg)
        {
            dmg.OnDeath += OnImpact;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnImpact();
    }

    private void OnImpact()
    {
        Instantiate(impactParticles, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
}
