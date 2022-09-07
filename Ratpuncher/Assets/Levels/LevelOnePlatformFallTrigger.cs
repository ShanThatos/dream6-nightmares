using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOnePlatformFallTrigger : MonoBehaviour
{

    public Rigidbody2D platformRb;
    bool triggered = false;

    void dropPlatform() {
        platformRb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("moveCameraOver", 4f);
    }
    void moveCameraOver() {
        MainCameraScript.instance.maxX = 1000;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && !triggered) {
            triggered = true;
            GetComponent<AudioSource>().Play();
            Invoke("dropPlatform", .5f);
        }
    }
}
