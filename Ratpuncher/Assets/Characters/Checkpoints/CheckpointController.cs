using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public Animator lightAnimator;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.instance.currentCheckpoint = transform.position;

            if (lightAnimator)
                lightAnimator.SetTrigger("Start");
        }
            

        
    }
}
