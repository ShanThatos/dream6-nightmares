using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameManager : MonoBehaviour {
    
    [HideInInspector]
    public GameObject player;
    public static GameManager instance { get; private set; }

    // private bool movementLocked = false;
    private bool isPaused = false;
    public GameObject pausePanel;
    private List<FreezeOnGMLock> freezeObjects = new List<FreezeOnGMLock>();


    public bool useTestPos;
    public Vector2 playerTestPos;


    [HideInInspector]
    public Vector2 currentCheckpoint;

    private int movementLockCount = 0;
    void Awake() {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        player = FindObjectOfType<PlayerMovement>().gameObject;
        currentCheckpoint = player.transform.position;

        if (useTestPos)
            player.transform.position = playerTestPos;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) { UnpauseGame(); }
            else { PauseGame(); }
        }
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

    // public static void SetMovementLock(bool locked) {
    //     if (instance == null) return;
    //     // PlayerInput playerInput = instance.player.GetComponent<PlayerInput>();
    //     // if (locked)
    //     // {
    //     //     // playerInput.enabled = false;
    //     //     instance.movementLockCount++;
    //     // }
    //     // else
    //     // {
    //     //     // playerInput.enabled = true;
    //     //     instance.movementLockCount--;
    //     // }

    //     // bool shouldLock = false;

    //     // if(instance.movementLockCount > 0)
    //     // {
    //     //     shouldLock = true;
    //     // }



    //     // instance.movementLocked = shouldLock;
    //     // playerInput.enabled = !shouldLock;

    //     // instance.freezeObjects.ForEach(x => { 
    //     //     if (shouldLock) x.Lock();
    //     //     else x.Unlock();
    //     // });

    //     instance.movementLockCount = Math.Max(0, instance.movementLockCount + (locked ? 1 : -1));
    //     bool shouldLock = instance.movementLockCount > 0;
    //     instance.movementLocked = shouldLock;

    //     instance.freezeObjects.ForEach(x => {
    //         if (shouldLock) x.Lock();
    //         else x.Unlock();
    //     });

    //     instance.player.GetComponent<PlayerMovement>().LockControls(shouldLock);


    // }
    
    public static void LockMovement() {
        if (instance == null) return;
        
        instance.movementLockCount++;
        if (instance.movementLockCount == 1) {
            instance.freezeObjects.ForEach(x => x.Lock());
            LockPlayer();
        }
    }
    public static void UnlockMovement() {
        if (instance == null) return;
        
        instance.movementLockCount--;
        if (instance.movementLockCount == 0) {
            instance.freezeObjects.ForEach(x => x.Unlock());
            UnlockPlayer();
        } else if (instance.movementLockCount < 0)
            instance.movementLockCount = 0;
    }

    public static void LockPlayer() {
        instance.player.GetComponent<PlayerMovement>().LockControls(true);
    }
    public static void UnlockPlayer() {
        instance.player.GetComponent<PlayerMovement>().LockControls(false);
    }

    public static bool IsMovementLocked() {
        return instance.movementLockCount > 0;
    }


    public static void RespawnPlayer() {
        instance.player.transform.position = instance.currentCheckpoint;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            RespawnPlayer();
    }


    public static void PauseGame() {
        if (instance.isPaused) return;
        instance.pausePanel.SetActive(true);
        instance.isPaused = true;
        Time.timeScale = 0f;
        LockMovement();
    }
    public static void UnpauseGame() {
        if (!instance.isPaused) return;
        instance.pausePanel.SetActive(false);
        instance.isPaused = false;
        UnlockMovement();
        Time.timeScale = 1f;
    }

    private void OnDrawGizmos() {
        if (useTestPos) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(playerTestPos, 0.5f);
        }
    }
}