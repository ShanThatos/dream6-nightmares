using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybirdBossfightController : MonoBehaviour {

    BugFishController bugfish;

    public Animator ladybirdAnimator;
    public Animator bugfishRevealAnimator;
    public Rigidbody2D fallingDock;
    public AudioSource bossfightMusic;
    public AudioSource seaMusic;
    public GameObject[] barriers;

    bool started = false;
    bool ended = false;

    void Start() {
        bugfish = GameObject.FindObjectOfType<BugFishController>();
        bugfish.setAnimating(true);
        foreach (GameObject obj in barriers)
        {
            obj.SetActive(false);
        }
    }

    public void StartBossfight() {
        if (!started) {
            started = true;
            StartCoroutine(StartBossFightCoroutine());
        }
    }

    public IEnumerator StartBossFightCoroutine() {
        Rigidbody2D playerRb = GameManager.instance.player.GetComponent<Rigidbody2D>();
        RigidbodyConstraints2D playerConstraints = playerRb.constraints;
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1.5f);

        ladybirdAnimator.Play("LBBFFade");
        yield return new WaitForSeconds(3f);

        MainCameraScript.instance.setCamPosLerp(.5f);
        MainCameraScript.instance.setCamSizeLerp(.5f);
        bugfish.transform.parent = bugfishRevealAnimator.transform;
        bugfish.transform.localPosition = Vector3.zero;
        bugfish.setAnimating(true);
        Vector2 cameraOffset = MainCameraScript.instance.cameraOffset;
        MainCameraScript.instance.target = bugfish.transform;
        MainCameraScript.instance.cameraOffset = new Vector2(0, 6);
        MainCameraScript.instance.camSizeMultiplier = 3.5f;

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

        fallingDock.bodyType = RigidbodyType2D.Dynamic;
        bugfish.HPBar.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        
        MainCameraScript.instance.target = GameManager.GetPlayerTransform();
        MainCameraScript.instance.camSizeMultiplier = 1f;

        bugfish.transform.parent = null;
        bugfish.setAnimating(false);

        foreach (GameObject obj in barriers)
        {
            obj.SetActive(true);
        }

        bugfish.resetStates();
        playerRb.constraints = playerConstraints;

        MainCameraScript.instance.cameraOffset = cameraOffset;
        MainCameraScript.instance.setCamPosLerp(MainCameraScript.instance.defaultCamPosLerp);
        MainCameraScript.instance.setCamSizeLerp(MainCameraScript.instance.defaultCamSizeLerp);
    }

    public void EndBossFight() {
        if (!ended) {
            ended = true;
            StartCoroutine(EndBossFightCoroutine());
        }
    }

    public IEnumerator EndBossFightCoroutine() {
        bugfishRevealAnimator.Play("BFDone");
        bugfish.HPBar.gameObject.SetActive(false);

        foreach (GameObject obj in barriers)
        {
            obj.SetActive(false);
        }

        // GameManager.SetMovementLock(true);
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetInt("LadybirdSolved", 1);
        DialogueManager.instance.PlayDialogue("DefeatBugfish");
    }
}
