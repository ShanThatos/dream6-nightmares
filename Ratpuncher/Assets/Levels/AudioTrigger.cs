using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    bool triggered = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && !triggered) {
            triggered = true;
            GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && triggered) {
            triggered = false;
            GetComponent<AudioSource>().Stop();
        }
    }
}
