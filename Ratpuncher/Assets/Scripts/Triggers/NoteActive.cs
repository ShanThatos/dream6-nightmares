using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteActive : MonoBehaviour
{
    public string Item;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(Item, 0) == 1)
        {
            this.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
