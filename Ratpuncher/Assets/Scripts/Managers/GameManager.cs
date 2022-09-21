using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public static GameManager instance { get; private set; }

    private bool movementLocked = false;
    private List<FreezeOnGMLock> freezeObjects = new List<FreezeOnGMLock>();

    void Awake() {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }


    public static Transform GetPlayerTransform() {
        return instance.player.transform;
    }



    public void __AddMovementLockedObject(FreezeOnGMLock x) {
        if (freezeObjects.Contains(x)) return;
        freezeObjects.Add(x);
    }
    public static void AddMovementLockedObject(FreezeOnGMLock x) {
        instance.__AddMovementLockedObject(x);
    }

    public void __RemoveMovementLockedObject(FreezeOnGMLock x) {
        int index = freezeObjects.IndexOf(x);
        if (index != -1) {
            freezeObjects.RemoveAt(index);
            x.Unlock();
        }
    }
    public static void RemoveMovementLockedObject(FreezeOnGMLock x) {
        instance.__RemoveMovementLockedObject(x);
    }

    public void __SetMovementLock(bool locked) {
        if (movementLocked == locked) return;
        movementLocked = locked;

        freezeObjects.ForEach(x => { 
            if (locked) x.Lock();
            else x.Unlock();
        });
    }
    public static void SetMovementLock(bool locked) {
        instance.__SetMovementLock(locked);
    }
    public static bool IsMovementLocked() {
        return instance.movementLocked;
    }
}