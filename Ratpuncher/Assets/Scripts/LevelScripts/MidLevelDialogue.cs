using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MidLevelDialogue : MonoBehaviour
{

    public string dialogue;

    bool alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || alreadyTriggered)
        {
            return;
        }

        alreadyTriggered = true;
        DialogueManager.instance.PlayDialogue(dialogue);
    }
}
