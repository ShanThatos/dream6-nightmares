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
                    if (PlayerPrefs.GetInt(alternativeDialogue, 0) == 0)
                    {
                        DialogueManager.instance.PlayDialogue(alternativeDialogue);
                        PlayerPrefs.SetInt(alternativeDialogue, 1);
                        isTriggered = true;
                        return;
                    }
                }
            }
            if (PlayerPrefs.GetInt(startDialogue, 0) == 0)
            {
                // Play the start dialogue if none of the conditions matches
                DialogueManager.instance.PlayDialogue(startDialogue);
                isTriggered = true;
                PlayerPrefs.SetInt(startDialogue, 1);
            }
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
