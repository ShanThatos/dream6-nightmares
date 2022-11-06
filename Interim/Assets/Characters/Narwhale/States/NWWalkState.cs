using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NWWalkState : NWState {

    public override void enter() {
        controller.animator.Play("NarwhaleIdle");
    }

    public override void run() {}

    public override string getStateName() {
        return "NWWalk";
    }
}
