using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OfficeManager : MonoBehaviour
{
    private HubDialogueController hubControlScript;
    private bool triggeredDia;
    // Start is called before the first frame update
    void Start()
    {
        triggeredDia = false;
        hubControlScript = GameObject.FindGameObjectWithTag("HubDialogue").GetComponent<HubDialogueController>();
        //hubControlScript.check("Connecting to HubDialogueController");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggeredDia)
        {
            hubControlScript.StartDialogue(hubControlScript.hubDialogueSO.firstDialogues);
            triggeredDia = true;
        }

    }
}
