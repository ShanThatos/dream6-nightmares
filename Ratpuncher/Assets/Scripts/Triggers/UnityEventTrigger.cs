using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTrigger : MonoBehaviour {
    public bool onlyTriggerOnce = false;
    bool canTrigger = true;
    public UnityEvent triggerResponse;

    public void OnTriggerEnter2D(Collider2D other) {
        if (canTrigger && other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            triggerResponse.Invoke();
            canTrigger = !onlyTriggerOnce;
        }
    }
}
