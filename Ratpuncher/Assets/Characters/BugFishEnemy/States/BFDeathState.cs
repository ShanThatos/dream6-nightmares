using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFDeathState : BFState {

    public override void enter() {
        // controller.animator.Play("BFDeath");
        Destroy(controller.gameObject);
    }

    public override void run() {

    }

    public override string getStateName() {
        return "BFDeath";
    }
}

