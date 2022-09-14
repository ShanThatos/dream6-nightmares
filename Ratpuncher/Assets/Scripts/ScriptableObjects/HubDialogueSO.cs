using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


[CreateAssetMenu(fileName = "HubDialogue", menuName = "ScriptableObjects/HubDialogue")]
public class HubDialogueSO : ScriptableObject
{
    public Character[] character;

    [TextArea(10, 15)]
    public String[] firstDialogues;
    [TextArea(10, 15)]
    public String[] secondDialogues;

    [Serializable]
    public class Character
    {
        public String name;
        public Sprite sprite;
    }
}
