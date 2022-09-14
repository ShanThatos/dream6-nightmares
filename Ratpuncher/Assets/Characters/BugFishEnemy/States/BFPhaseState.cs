using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BFPhaseState : BFState {

    public override void enter() {
        controller.animator.Play("BFPhase");
    }

    public override void run() {

    }

    public override string getStateName() {
        return "BFPhase";
    }
}

