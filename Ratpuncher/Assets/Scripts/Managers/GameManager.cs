using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public static GameManager instance { get; private set; }

    private bool movementLocked = false;
    private bool isPaused = false;
    public GameObject pausePanel;
    private List<FreezeOnGMLock> freezeObjects = new List<FreezeOnGMLock>();


    public Vector2 currentCheckpoint;

    private int movementLockCount = 0;
    void Awake() {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        player = FindObjectOfType<PlayerMovement>().gameObject;
        currentCheckpoint = player.transform.position;
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

    public static void SetMovementLock(bool locked) {
        if (instance == null) return;
        PlayerInput playerInput = instance.player.GetComponent<PlayerInput>();
        if (locked)
        {
            // playerInput.enabled = false;
            instance.movementLockCount++;
        }
        else
        {
            // playerInput.enabled = true;
            instance.movementLockCount--;
        }

        bool shouldLock = false;

        if(instance.movementLockCount > 0)
        {
            shouldLock = true;
        }

        instance.movementLocked = shouldLock;
        playerInput.enabled = !shouldLock;

        instance.freezeObjects.ForEach(x => { 
            if (shouldLock) x.Lock();
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


    public static void PauseGame() {
        if (instance.isPaused) return;
        instance.pausePanel.SetActive(true);
        instance.isPaused = true;
        Time.timeScale = 0f;
        SetMovementLock(true);
    }
    public static void UnpauseGame() {
        if (!instance.isPaused) return;
        instance.pausePanel.SetActive(false);
        instance.isPaused = false;
        SetMovementLock(false);
        Time.timeScale = 1f;
    }
}