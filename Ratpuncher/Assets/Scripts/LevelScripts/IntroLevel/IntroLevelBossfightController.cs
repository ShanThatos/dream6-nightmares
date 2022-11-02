using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroLevelBossfightController : MonoBehaviour {

    bool started = false;
    bool ended = false;

    RemnantController remnant;
    Animator animator;

    void Start() {
        remnant = GameObject.FindObjectOfType<RemnantController>();
        animator = GetComponent<Animator>();
    }

    public void StartBossfight() {
        if (!started) {
            started = true;
            StartCoroutine(StartBossFightCoroutine());
        }
    }

    public IEnumerator StartBossFightCoroutine() {
        PlayerMovement player = GameManager.instance.player.GetComponent<PlayerMovement>();
        Damagable playerDamagable = player.GetComponent<Damagable>();
        GameManager.LockPlayer();
        
        yield return new WaitForSeconds(1f);
        GameManager.UnlockPlayer();
        GameManager.LockPlayer();

        DialogueManager.instance.PlayDialogue("IntroBeforeBoss");
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => DialogueManager.instance.isDialogueFinished);

        animator.Play("IntroBossfightWithElio");
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("IntroBossfightWithElio") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f);

        yield return new WaitForSeconds(1f);

        switchRemnantState("RMScream");
        yield return new WaitForSeconds(1.4f);

        //DialogueManager.instance.PlayDialogue("IntroClosure");
        ScenesTransition.instance.ChangeScene("Office");
    }

    public void switchRemnantState(string state) {
        remnant.switchState(state);
    }

    public void EndBossFight() {
        if (!ended) {
            ended = true;
            StartCoroutine(EndBossFightCoroutine());
        }
    }

    public IEnumerator EndBossFightCoroutine() {
        
        yield return new WaitForSeconds(1f);
    }
}
