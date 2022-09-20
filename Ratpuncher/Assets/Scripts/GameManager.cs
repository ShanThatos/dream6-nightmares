using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public static GameManager instance { get; private set; }

    public bool movementLocked { get; private set; }
    private List<Rigidbody2D> bodies = new List<Rigidbody2D>();

    void Awake() {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }


    public static Transform GetPlayerTransform() {
        return instance.player.transform;
    }

    public static void SetMovementLock(bool locked) {
        instance.movementLocked = locked;
        instance.bodies.ForEach(body => body.freezeRotation = locked);
    }
}