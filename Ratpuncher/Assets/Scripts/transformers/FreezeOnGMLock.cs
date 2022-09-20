using UnityEngine;

public class FreezeOnGMLock : MonoBehaviour {
    void Start() {
        GameManager.AddMovementLockedBody(GetComponent<Rigidbody2D>());
    }

    void OnDestroy() {
        GameManager.RemoveMovementLockedBody(GetComponent<Rigidbody2D>());
    }
}
