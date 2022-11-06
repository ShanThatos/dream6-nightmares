using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {
    public void Start() {}

    public virtual void init() {}

    public virtual bool canEnter() { return false; }
    public virtual void enter() {}
    public virtual void exit() {}
    public virtual void run() {}

    public abstract string getStateName();


    protected StateManager manager;
    public void setManager(StateManager manager) {
        this.manager = manager;
    }
}