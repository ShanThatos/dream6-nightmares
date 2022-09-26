using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    private DialogueController dialogueScript;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        dialogueScript = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueController>();
    }
    
    public void PlayDialogue(string dialogueName)
    {
        for (int i = 0; i < dialogueScript.dialogueSystem.dialogues.Length; i++)
        {
            if (dialogueName == dialogueScript.dialogueSystem.dialogues[i].dialogueName)
            {
                if (dialogueScript.dialogueSystem.dialogues[i].needsTransition)
                {
                    dialogueScript.soSceneName = dialogueScript.dialogueSystem.dialogues[i].sceneName;
                }
                else
                {
                    dialogueScript.soSceneName = "";
                }
                dialogueScript.StartDialogue(dialogueScript.dialogueSystem.dialogues[i].dialoguesText);
                Debug.Log("Triggered");
            }
        }
    }

    [ContextMenu("Reset Ladybird Level")]
    public void ResetLadybird()
    {
        PlayerPrefs.SetInt("LadybirdSolved", 0);
    }
}