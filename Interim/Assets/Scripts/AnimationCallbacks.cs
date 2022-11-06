using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallbacks : MonoBehaviour
{
    public PlayerAttackManager attackManager;
    public PlayerMovement movement;

    // End animation lock, do other attacks if needed
    public void FinishAttack(int comboStage)
    {
        attackManager.endAnimLock();
    }

    // End attack
    public void EndRecovery()
    {
        attackManager.EndAttack();
    }

    // Spawn particles and hitbox()
    public void SpawnParticlesAndHitbox()
    {
        attackManager.SpawnAttack();
    }

    public void DeathAnimDone()
    {
        movement.OnDeathAnimDone();
    }

    public void RespawnAnimDone()
    {
        movement.OnRespawnAnimDone();
    }

    public void ChargeAttackReady()
    {
        attackManager.ChargedAttackReady();
    }
}
