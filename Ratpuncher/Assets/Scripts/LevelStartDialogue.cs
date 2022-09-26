using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelStartDialogue : MonoBehaviour
{
    public string startDialogue;
    private string alternativeDialogue;
    private bool isTriggered;
    public void Start()
    {
        isTriggered = false;
    }

    public void Update()
    {
        /* for testing
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("LadybirdSolved", 1);
            DialogueManager.instance.PlayDialogue("DefeatBugfish");
            Debug.Log("Defeated Bugfish");
        }
        */
        if (!isTriggered)
        {
            if (SceneManager.GetActiveScene().name == "Office")
            {
                // if ladybird level is solved, show solved dialogue
                if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 1)
                {
                    alternativeDialogue = "LadybirdClosure";
                    DialogueManager.instance.PlayDialogue(alternativeDialogue);
                    isTriggered = true;
                    return;
                }
            }
            
            // Play the start dialogue if none of the conditions matches
            DialogueManager.instance.PlayDialogue(startDialogue);
            isTriggered = true;
        }
    }
}
