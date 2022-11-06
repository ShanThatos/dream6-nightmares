using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    public DialogueController dialogueScript;
    public bool isDialogueOn;
    public bool isDialogueFinished;

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

    private void Update()
    {
        isDialogueFinished = dialogueScript.isDialogueFinished;
        isDialogueOn = dialogueScript.isDialogueOn;
    }

    public void PlayDialogue(string dialogueName)
    {
        GameManager.LockMovement();

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
                // Debug.Log("Triggered");
            }
        }

    }

    [ContextMenu("Reset Levels")]
    public void ResetLevels()
    {
        string rebinds = PlayerPrefs.GetString("rebinds");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("rebinds", rebinds);
    }
}
