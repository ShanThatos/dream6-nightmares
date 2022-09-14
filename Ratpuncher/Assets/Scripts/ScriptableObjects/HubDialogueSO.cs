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

    [Serializable]
    public class Character
    {
        public String name;
        public Sprite sprite;
    }
}
