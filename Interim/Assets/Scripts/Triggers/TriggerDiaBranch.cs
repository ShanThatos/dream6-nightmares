using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDiaBranch : MonoBehaviour
{
    private LevelStartDialogue levelStartScript;
    private void Start()
    {
        levelStartScript = GameObject.FindGameObjectWithTag("OfficeManager").GetComponent<LevelStartDialogue>();
    }
    public void PlayBranchingDia()
    {
        if (gameObject.name == "RemBranchSave")
        {
            DialogueManager.instance.dialogueScript.EndDialogueFunction.AddListener(TriggerFinish);
        }
        else if (gameObject.name == "RemBranchFail")
        {
            PlayerPrefs.SetInt("Failed", 1);
            PlayerPrefs.SetInt("RemIntro", 0);
        }
        DialogueManager.instance.PlayDialogue(gameObject.name);
    }
    public void TriggerFinish()
    {
        levelStartScript.TriggerFinishedAction();
    }
}
