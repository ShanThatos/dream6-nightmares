using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneResetTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
            SceneManager.LoadScene("MainMenu");
    }
}
