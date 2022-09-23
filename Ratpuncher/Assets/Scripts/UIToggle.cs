using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{

    public GameObject toggle;
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    
    public void ToggleIt()
    {
        bool isActive = toggle.activeInHierarchy;

        isActive = !isActive;
        toggle.SetActive(isActive);

        if (isActive)
        {
            text.text = "X";
        }
        else
        {
            text.text = "O";
        }
    }
}
