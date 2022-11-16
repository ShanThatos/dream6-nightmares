using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelActivate : MonoBehaviour
{
    public string conditionCheck;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt(conditionCheck, 0) == 1)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

}
