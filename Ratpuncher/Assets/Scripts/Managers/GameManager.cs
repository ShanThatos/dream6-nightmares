using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public static GameManager instance { get; private set; }

    private bool movementLocked = false;
    private List<FreezeOnGMLock> freezeObjects = new List<FreezeOnGMLock>();


    public Vector2 currentCheckpoint;


    void Awake() {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        player = FindObjectOfType<PlayerMovement>().gameObject;
        currentCheckpoint = player.transform.position;
    }


    public static Transform GetPlayerTransform() {
        return instance.player.transform;
    }

    public static void AddMovementLockedObject(FreezeOnGMLock x) {
        if (instance.freezeObjects.Contains(x)) return;
        instance.freezeObjects.Add(x);
    }

    public static void RemoveMovementLockedObject(FreezeOnGMLock x) {
        int index = instance.freezeObjects.IndexOf(x);
        if (index != -1) {
            instance.freezeObjects.RemoveAt(index);
            x.Unlock();
        }
    }

    public static void SetMovementLock(bool locked) {
        if (instance.movementLocked == locked) return;
        instance.movementLocked = locked;

        instance.freezeObjects.ForEach(x => { 
            if (locked) x.Lock();
            else x.Unlock();
        });
    }
    
    public static bool IsMovementLocked() {
        return instance.movementLocked;
    }



    public static void RespawnPlayer() {
        instance.player.transform.position = instance.currentCheckpoint;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            RespawnPlayer();
    }
}