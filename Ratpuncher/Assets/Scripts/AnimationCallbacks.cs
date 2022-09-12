using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallbacks : MonoBehaviour
{
    public PlayerAttackManager attackManager;
    public void FinishAttack(int comboStage)
    {
        Debug.Log("Finish " + comboStage);
        attackManager.endAttackAnim(comboStage);
    }
}
