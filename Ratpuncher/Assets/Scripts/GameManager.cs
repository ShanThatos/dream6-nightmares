using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public static GameManager instance { get; private set; }
    void Awake() {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }


    public static Transform getPlayerTransform() {
        return instance.player.transform;
    }
}