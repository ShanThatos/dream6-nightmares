using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneBossFightCameraTrigger : MonoBehaviour
{

    public Vector2 playerCameraOffset = new Vector2(0, 2);
    bool triggered = false;
    Vector2 oldCameraOffset;

    void OnTriggerEnter2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && !triggered) {
            triggered = true;
            oldCameraOffset = MainCameraScript.instance.playerCameraOffset;
            MainCameraScript.instance.playerCameraOffset = playerCameraOffset;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && triggered) {
            triggered = false;
            MainCameraScript.instance.playerCameraOffset = oldCameraOffset;
        }
    }
}
