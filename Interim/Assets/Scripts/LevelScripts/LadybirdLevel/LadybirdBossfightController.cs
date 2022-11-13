using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LadybirdBossfightController : MonoBehaviour {

    BugFishController bugfish;

    public Animator ladybirdAnimator;
    public Animator bugfishRevealAnimator;
    public Rigidbody2D fallingDock;
    public AudioSource bossfightMusic;
    public AudioSource seaMusic;
    public GameObject restartTrigger;
    public Transform bugfishRestartPoint;
    public GameObject[] barriers;
    public TutorialPopup GPPopup;

    bool started = false;
    bool ended = false;
    bool waitingForRestart = false;

    void Start() {
        bugfish = GameObject.FindObjectOfType<BugFishController>();
        bugfish.setAnimating(true);
        setBarriersActive(false);
    }

    public void StartBossfight() {
        if (!started) {
            started = true;
            StartCoroutine(StartBossFightCoroutine());
        }
    }

    public IEnumerator StartBossFightCoroutine() {
        Damagable playerDamagable = GameManager.instance.player.GetComponent<Damagable>();
        playerDamagable.OnRespawn += ResetBossFight;
        GameManager.LockPlayer();

        // MainCameraScript.instance.useSetting(0);
        // yield return new WaitForSeconds(5f);
        // MainCameraScript.instance.useDefaultPositioning();
        // yield return new WaitForSeconds(2f);
        // MainCameraScript.instance.useDefaultSetting();


        yield return new WaitForSeconds(1.5f);

        DialogueManager.instance.PlayDialogue("YoungLadybird");
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => DialogueManager.instance.isDialogueFinished);
        yield return new WaitForSeconds(1f);

        ladybirdAnimator.Play("LBBFFade");
        yield return new WaitForSeconds(3f);

        MainCameraScript.instance.setCamPosLerp(.5f);
        MainCameraScript.instance.setCamFOVLerp(.95f);
        bugfish.transform.parent = bugfishRevealAnimator.transform;
        bugfish.transform.localPosition = Vector3.zero;
        bugfish.setAnimating(true);
        Vector2 cameraOffset = MainCameraScript.instance.cameraOffset;
        MainCameraScript.instance.target = bugfish.transform;
        MainCameraScript.instance.cameraOffset = new Vector2(2, 2);
        MainCameraScript.instance.camFOVMultiplier = 2f;

        for (int i = 4; i >= 0; i--) {
            seaMusic.volume = i / 5f;
            yield return new WaitForSeconds(1f);
        }


        MainCameraScript.instance.setCamPosLerp(5f);

        bugfishRevealAnimator.Play("BFReveal");
        bossfightMusic.Play();
        while (true) {
            yield return new WaitForSeconds(0.2f);
            bugfish.resetStates();
            if (bugfishRevealAnimator.GetCurrentAnimatorStateInfo(0).IsName("BFReveal") && bugfishRevealAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) 
                break;
        }

        bugfish.HPBar.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        MainCameraScript.instance.useSetting(0);
        
        // MainCameraScript.instance.target = GameManager.GetPlayerTransform();
        // MainCameraScript.instance.camFOVMultiplier = 1f;

        bugfish.transform.parent = null;
        bugfish.setAnimating(false);

        setBarriersActive(true);

        bugfish.resetStates();

        GameManager.UnlockPlayer();

        // MainCameraScript.instance.cameraOffset = cameraOffset;
        // MainCameraScript.instance.setCamPosLerp(MainCameraScript.instance.defaultCamPosLerp);
        // MainCameraScript.instance.setCamFOVLerp(MainCameraScript.instance.defaultCamFOVLerp);
    }

    public void EndBossFight() {
        if (!ended) {
            ended = true;
            StartCoroutine(EndBossFightCoroutine());
        }
    }

    void setBarriersActive(bool active)
    {
        foreach (GameObject obj in barriers)
        {
            obj.SetActive(active);
        }
    }

    // Resets the bugfish's position and HP
    // Called when the player respawns (PlayerDamagable.OnRespawn)
    // so the player won't see this happen on screen
    void ResetBossFight()
    {
        MainCameraScript.instance.useDefaultSetting();
        setBarriersActive(false);
        restartTrigger.SetActive(true);

        AttackHitbox[] bossAttacks = bugfish.GetComponentsInChildren<AttackHitbox>();
        Debug.Log("RESET " + bossAttacks.Length);
        foreach(AttackHitbox attack in bossAttacks)
            attack.damage *= 0.75f;

        bugfish.getDamagable().Respawn();
        bugfish.switchState("BFIdle");
        bugfish.resetStates();
        

        waitingForRestart = true;

        Collider2D bfCollider = bugfish.GetComponent<Collider2D>();
        bfCollider.offset = new Vector2(bfCollider.offset.x, 0);

        Vector3 pos = bugfish.transform.position;
        pos.y += 2f;
        bugfish.transform.position = pos;

    }

    // Turns the barriers back on when the player reengages
    // in combat.
    public void RestartFight()
    {
        if (waitingForRestart)
        {
            MainCameraScript.instance.useSetting(0);
            setBarriersActive(true);
            restartTrigger.SetActive(false);
            waitingForRestart = false;
        }
    }

    public IEnumerator EndBossFightCoroutine() {

        GameManager.LockMovement();

        bugfish.getDamagable().setInvincibility(true);
        bugfishRevealAnimator.Play("BFDone");
        bugfish.HPBar.gameObject.SetActive(false);

        foreach (GameObject obj in barriers)
        {
            obj.GetComponent<Animator>().SetTrigger("stop");
        }

        // GameManager.SetMovementLock(true);
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetInt("LadybirdSolved", 1);

        GameManager.UnlockMovement();

        // Popup will play dialogue after it is closed
        GPPopup.Open();
    }
}
