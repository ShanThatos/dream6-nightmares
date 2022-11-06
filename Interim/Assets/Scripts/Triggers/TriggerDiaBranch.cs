using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDiaBranch : MonoBehaviour
{
    public void PlayBranchingDia()
    {
        DialogueManager.instance.PlayDialogue(gameObject.name);
    }
}
