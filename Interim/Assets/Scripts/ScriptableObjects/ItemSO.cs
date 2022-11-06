using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    public Items[] items;

    [Serializable]
    public class Items
    {
        public Sprite itemSprite;
        public String itemName;
        [TextArea(5, 10)]
        public String itemDescription;
    }
}
