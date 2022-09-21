using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform startPosition;
    public Transform endPosition;

    public float speed = 1f;

    float currentTime = 0f;

    void FixedUpdate() {
        if (GameManager.IsMovementLocked()) return;
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, Mathf.PingPong((currentTime += Time.deltaTime) * speed, 1f));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
            GameManager.instance.player.transform.parent = transform;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) 
            GameManager.instance.player.transform.parent = null;
    }
}
