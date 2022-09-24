using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[CreateAssetMenu(fileName = "DialogueSystem", menuName = "ScriptableObjects/DialogueSystem")]
public class DialogueSystem : ScriptableObject
{
    public Character[] character;
    public Dialogues[] dialogues;
    [Serializable]
    public class Character
    {
        public String spriteName;
        public Sprite sprite;
    }

    [Serializable]
    public class Dialogues
    {
        public String dialogueName;
        [TextArea(5, 10)]
        public String[] dialoguesText;
    }
}
