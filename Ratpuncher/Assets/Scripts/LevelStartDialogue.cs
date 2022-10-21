using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelStartDialogue : MonoBehaviour
{
    public string startDialogue;
    public bool hasFinishedAction;
    private string alternativeDialogue;
    private bool isTriggered;
    private bool didFinishedAction;
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

        if (isTriggered)
        {
            if (hasFinishedAction && DialogueManager.instance.isDialogueFinished)
            {
                TriggerFinishedAction();
            }

        }
    }

    private void TriggerFinishedAction()
    {
        if (!didFinishedAction)
        {
            if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 0)
            {
                this.gameObject.GetComponent<OfficeManager>().TriggerPin();
                this.gameObject.GetComponent<OfficeManager>().TriggerAlert();
                didFinishedAction = true;
            }
        }
    }
}
