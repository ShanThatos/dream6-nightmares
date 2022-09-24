using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OfficeManager : MonoBehaviour
{
    private bool isTriggered;
    public void Start()
    {
        isTriggered = false;
    }

    public void Update()
    {
        if (!isTriggered)
        {
            DialogueManager.instance.PlayDialogue("HubStart");
            isTriggered = true;
        }
    }
}
