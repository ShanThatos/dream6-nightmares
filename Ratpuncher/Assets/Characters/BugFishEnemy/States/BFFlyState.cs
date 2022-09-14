using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFFlyState : BFState {

    public override void enter() {
        controller.animator.Play("BFFly");
    }

    public override void run() {

    }

    public override string getStateName() {
        return "BFFly";
    }
}

