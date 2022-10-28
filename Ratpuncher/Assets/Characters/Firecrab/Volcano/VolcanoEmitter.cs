using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class VolcanoEmitter : MonoBehaviour
{

    int frameCount = 0;

    [Tooltip("Particle system used to init projectiles")]
    public ParticleSystem emitter;

    [Tooltip("Actual projectile prefab")]
    public GameObject projectile;

    [Tooltip("Number of projectiles")]
    public int count;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(frameCount == 10)
        {
            Particle[] particles = new Particle[count];
            emitter.GetParticles(particles);

            int i = 0;
            foreach(Particle p in particles)
            {
                GameObject obj = Instantiate(projectile, p.position, Quaternion.identity);

                if(obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    rb.velocity = p.velocity;
                }

                

                i++;
            }

            Debug.Log("Volcano launched " + i + " projectiles");
            Destroy(gameObject, 3f);
        }

        frameCount++;
    }
}
