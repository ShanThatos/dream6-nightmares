using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElioState : MonoBehaviour
{
    public OfficeManager officeManager;
    public void Interact()
    {
        string dialogueChoice = "ElioHub";
        if (PlayerPrefs.GetInt("LadybirdSolved", 0) == 0 && PlayerPrefs.GetInt("HubStart", 0) == 1)
        {
            dialogueChoice = "ElioHub";
        }
        else if (PlayerPrefs.GetInt("LadybirdClosure", 0) == 1 && PlayerPrefs.GetInt("ElioIntro", 0) == 1 && PlayerPrefs.GetInt("ElioStart", 0) == 0)
        {
            dialogueChoice = "ElioStart";
            DialogueManager.instance.dialogueScript.EndDialogueFunction.AddListener(TriggerFinished);
        }
        else if (PlayerPrefs.GetInt("ElioStart", 0) == 1 && PlayerPrefs.GetInt("ElioSolved", 0) == 0)
        {
            dialogueChoice = "ElioMid";
        }
        else if (PlayerPrefs.GetInt("RemIntro", 0) == 1 && PlayerPrefs.GetInt("RemSolved", 0) == 0)
        {
            dialogueChoice = "ElioHubFinal";
        }

        DialogueManager.instance.PlayDialogue(dialogueChoice);
        PlayerPrefs.SetInt(dialogueChoice, 1);
    }

    public void TriggerFinished()
    {
        Debug.Log("Trigger elio pin and alert");
        officeManager.TriggerPin("Elio's case is pinned to the board");
        officeManager.TriggerAlert();
        DialogueManager.instance.dialogueScript.EndDialogueFunction.RemoveAllListeners();
    }
}
