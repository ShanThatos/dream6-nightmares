using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElioState : MonoBehaviour
{
    public OfficeManager officeManager;
    private bool hasFinishedAction = false;
    private bool isTriggered;
    public void Interact()
    {
        string dialogueChoice = "ElioHub";
        if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 0 && PlayerPrefs.GetInt("HubStart", 0) == 1)
        {
            dialogueChoice = "ElioHub";
            hasFinishedAction = false;
        }
        else if (PlayerPrefs.GetInt("LadybirdClosure", 0) == 1 && PlayerPrefs.GetInt("ElioIntro", 0) == 1 && PlayerPrefs.GetInt("ElioStart", 0) == 0)
        {
            dialogueChoice = "ElioStart";
            hasFinishedAction = true;
        }
        else if (PlayerPrefs.GetInt("ElioStart", 0) == 1 && PlayerPrefs.GetInt("ElioSolved", 0) == 0)
        {
            dialogueChoice = "ElioMid";
            hasFinishedAction = false;
        }

        DialogueManager.instance.PlayDialogue(dialogueChoice);
        PlayerPrefs.SetInt(dialogueChoice, 1);
        isTriggered = true;
        if (isTriggered)
        {
            if (hasFinishedAction && DialogueManager.instance.isDialogueFinished)
            {
                Debug.Log("Trigger elio pin and alert");
                officeManager.TriggerPin("Elio's case is pinned to the board");
                officeManager.TriggerAlert();
                isTriggered = false;
            }
        }
    }

}
