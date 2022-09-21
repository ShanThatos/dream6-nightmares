using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    public bool playOnlyOnce = false;
    public bool playOnEnter = true;
    public bool stopOnExit = true;
    bool triggered = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && !triggered) {
            triggered = true;
            if (playOnEnter)
                GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && triggered) {
            if (!playOnlyOnce)
                triggered = false;
            if (stopOnExit)
                GetComponent<AudioSource>().Stop();
        }
    }
}
