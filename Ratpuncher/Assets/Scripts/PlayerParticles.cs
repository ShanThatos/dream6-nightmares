using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{

    public GameObject jumpParticles;
    public GameObject dashParticles;
    public GameObject poundParticles;

    public Transform bottom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnJumpParticles()
    {
        Instantiate(jumpParticles, bottom.position, Quaternion.identity);
    }

    public void spawnDashParticles(bool flip = false)
    {
        
        GameObject particles = Instantiate(dashParticles, bottom.position, Quaternion.identity);
        if (flip)
        {
            // Flip particle if dashing left
            particles.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        }
    }

    public void spawnPoundParticles()
    {
        Instantiate(poundParticles, bottom.position, Quaternion.identity);
    }
}
