using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {

    public abstract string getStateName();

    public virtual void onEnter() {}
    public virtual void onExit() {}

    public abstract State run();
}