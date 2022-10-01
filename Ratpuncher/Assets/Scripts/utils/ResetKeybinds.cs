using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetKeybinds : MonoBehaviour
{
    [SerializeField]
    InputActionAsset actions;

    public void ResetAll()
    {
        foreach (InputActionMap map in actions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
            PlayerPrefs.DeleteKey("rebinds");
        }
    }
}
