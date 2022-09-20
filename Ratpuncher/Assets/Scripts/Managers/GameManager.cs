using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public static GameManager instance { get; private set; }

    private bool movementLocked = false;
    private List<Rigidbody2D> movementLockBodies = new List<Rigidbody2D>();
    private List<RigidbodyConstraints2D> movementLockPreviousConstraints = new List<RigidbodyConstraints2D>();

    void Awake() {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }


    public static Transform GetPlayerTransform() {
        return instance.player.transform;
    }



    public void __AddMovementLockedBody(Rigidbody2D body) {
        movementLockBodies.Add(body);
        movementLockPreviousConstraints.Add(body.constraints);
        if (movementLocked)
            body.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public static void AddMovementLockedBody(Rigidbody2D body) {
        instance.__AddMovementLockedBody(body);
    }

    public void __RemoveMovementLockedBody(Rigidbody2D body) {
        int index = movementLockBodies.IndexOf(body);
        if (index != -1) {
            movementLockBodies.RemoveAt(index);
            body.constraints = movementLockPreviousConstraints[index];
            movementLockPreviousConstraints.RemoveAt(index);
        }
    }
    public static void RemoveMovementLockedBody(Rigidbody2D body) {
        instance.__RemoveMovementLockedBody(body);
    }

    public void __SetMovementLock(bool locked) {
        if (movementLocked == locked) return;
        movementLocked = locked;

        for (int i = 0; i < movementLockBodies.Count; i++) {
            if (locked) {
                movementLockPreviousConstraints[i] = movementLockBodies[i].constraints;
                movementLockBodies[i].constraints = RigidbodyConstraints2D.FreezeAll;
            } else
                movementLockBodies[i].constraints = movementLockPreviousConstraints[i];
        }
    }
    public static void SetMovementLock(bool locked) {
        instance.__SetMovementLock(locked);
    }
    public static bool IsMovementLocked() {
        return instance.movementLocked;
    }
}