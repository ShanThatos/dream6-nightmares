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
        PlayerPrefs.SetInt("Failed", 0);
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
                if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 1 && PlayerPrefs.GetInt("LadybirdClosure", 0) == 0)
                {
                    alternativeDialogue = "LadybirdClosure";
                    PlayAlternativeDialouge();
                }
                else if (PlayerPrefs.GetInt("LadybirdClosure", 0) == 1 && PlayerPrefs.GetInt("ElioSolved", 0) == 0)
                {
                    alternativeDialogue = "ElioIntro";
                    PlayAlternativeDialouge();
                }
                else if (PlayerPrefs.GetInt("ElioSolved", 0) == 1 && PlayerPrefs.GetInt("ElioClosure", 0) == 0)
                {
                    alternativeDialogue = "ElioClosure";
                    PlayAlternativeDialouge();
                }
                else if (PlayerPrefs.GetInt("ElioClosure", 0) == 1 && PlayerPrefs.GetInt("RemSolved", 0) == 0)
                {
                    alternativeDialogue = "RemIntro";
                    PlayAlternativeDialouge();
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
                hasFinishedAction = false;
            }
        }
    }

    public void PlayAlternativeDialouge()
    {
        if (PlayerPrefs.GetInt(alternativeDialogue, 0) == 0)
        {
            DialogueManager.instance.PlayDialogue(alternativeDialogue);
            PlayerPrefs.SetInt(alternativeDialogue, 1);
            isTriggered = true;
            return;
        }
    }

    public void TriggerFinishedAction()
    {
        if (!didFinishedAction)
        {
            if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 0 && PlayerPrefs.GetInt("HubStart", 0) == 1)
            {
                this.gameObject.GetComponent<OfficeManager>().TriggerPin("Ladybird's case is pinned to the board");
                this.gameObject.GetComponent<OfficeManager>().TriggerAlert();
            }
            else if (PlayerPrefs.GetInt("ElioSolved", 0) == 0 && PlayerPrefs.GetInt("ElioIntro", 0) == 1 )
            {
                this.gameObject.GetComponent<OfficeManager>().TriggerAlert();
            }
            else if (PlayerPrefs.GetInt("RemSolved", 0) == 0 && PlayerPrefs.GetInt("RemIntro", 0) == 1)
            {
                this.gameObject.GetComponent<OfficeManager>().TriggerPin("Rem's case is pinned to the board");
                this.gameObject.GetComponent<OfficeManager>().TriggerAlert();
            } 
            didFinishedAction = true;
            Debug.Log("Triggered Finished Action");
            LeanTween.delayedCall(gameObject, 5f, () =>
            {
                didFinishedAction = false;
            });
        }
        DialogueManager.instance.dialogueScript.EndDialogueFunction.RemoveAllListeners();
    }
}
