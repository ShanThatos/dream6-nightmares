using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Item
{
    [Header("DONT EDIT ABOVE ATTRIBUTES")]
    public string KeyPlayerPref;

    public AudioSource lockedAudio;
    public SpatialFloatingTextController lockedText;
    public Animator openAnimator;

    bool isOpen;

    public void Start()
    {
        PlayerPrefs.SetInt(KeyPlayerPref, -1);
    }

    public override void OpenIdentifier()
    {
        if (isOpen)
        {
            return;
        }

        int canOpen = PlayerPrefs.GetInt(KeyPlayerPref, -1);
        if(canOpen == 1)
        {
            // Open
            openAnimator.SetTrigger("Open");
            isOpen = true;
        }
        else
        {
            //Locked
            lockedText.StartFloatingText();
            lockedAudio.Play();
        }
    }

    public override void CloseIdentifier()
    {
        return;
    }

    public override void Collect()
    {
        return;
    }
}
