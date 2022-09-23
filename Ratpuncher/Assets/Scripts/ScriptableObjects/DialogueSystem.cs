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
    public class SingleDia
    {
        public String displayName;
        public String spriteName;
        [TextArea(10, 15)]
        public String dialoguesText;
    }

    [Serializable]
    public class Dialogues
    {
        public String dialogueName;
        public SingleDia[] singleDia;
    }
}
