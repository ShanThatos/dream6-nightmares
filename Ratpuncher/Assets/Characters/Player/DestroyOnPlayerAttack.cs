using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerAttack : MonoBehaviour
{
    bool alreadyDestroyed = false;
    public AudioSource audioSource;
    public ParticleSystem particles;

    void OnTriggerStay2D(Collider2D other) {
        GameObject hit = other.gameObject;
        if (LayerMask.LayerToName(hit.layer) == "PlayerAttack") {
            if (!alreadyDestroyed) {
                if (audioSource != null)
                    audioSource.Play();

                if(particles != null)
                    particles.Play();
                Destroy(gameObject);
                alreadyDestroyed = true;
            }
        }
    }
}
