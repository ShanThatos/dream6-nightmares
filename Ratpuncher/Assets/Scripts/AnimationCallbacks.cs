using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallbacks : MonoBehaviour
{
    public PlayerAttackManager attackManager;
    
    // End animation lock, do other attacks if needed
    public void FinishAttack(int comboStage)
    {
        attackManager.endAnimLock(comboStage);
    }

    // End attack
    public void EndRecovery()
    {
        attackManager.EndAttack();
    }

}
