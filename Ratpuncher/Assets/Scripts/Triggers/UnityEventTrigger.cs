using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTrigger : MonoBehaviour {
    public bool onlyTriggerOnce = false;
    bool canTriggerEnter = true;
    bool canTriggerExit = true;
    public UnityEvent onEnterEvent;
    public UnityEvent onExitEvent;

    private void OnTriggerEnter2D(Collider2D other) {
        if (canTriggerEnter && other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            onEnterEvent.Invoke();
            canTriggerEnter = !onlyTriggerOnce;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (canTriggerExit && other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            onExitEvent.Invoke();
            canTriggerExit = !onlyTriggerOnce;
        }
    }
}
