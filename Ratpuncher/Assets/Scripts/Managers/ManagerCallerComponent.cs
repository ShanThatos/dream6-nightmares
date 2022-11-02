using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCallerComponent : MonoBehaviour {
    



    public void CallGMLockPlayer() {
        GameManager.LockPlayer();
    }

    public void CallGMUnlockPlayer() {
        GameManager.UnlockPlayer();
    }


}