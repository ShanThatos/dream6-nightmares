using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCFightController : MonoBehaviour
{
    public FirecrabController controller;

    public Damagable damagable;

    public Damagable player;

    public GameObject[] barriers;

    public Animator musicAnim;

    public GameObject healthBar;

    private Vector3 startPos;
    private bool active = false;
    private bool musicOn = false;

    public void Start()
    {
       startPos = controller.gameObject.transform.position;
       player.OnRespawn += ResetFight;
       damagable.OnDeath += EndFight;
       setBarriers(false);
    }

    public void StartFight()
    {
        if (active)
        {
            return;
        }

        controller.enabled = true;
        active = true;
        
        if (!musicOn)
        {
            musicAnim.Play("MusicFadeIn");
            healthBar.SetActive(true);
            musicOn = true;
        }
        setBarriers(true);
    }

    public void ResetFight()
    {
        active = false;
        controller.switchState("FCIdle");
        controller.gameObject.transform.position = startPos;
        controller.enabled = false;
        damagable.Respawn();
        setBarriers(false);
    }

    private void EndFight()
    {
        StartCoroutine(EndFightCoroutine());
    }

    private void setBarriers(bool active)
    {
        foreach(GameObject obj in barriers){
            obj.SetActive(active);
        }
    }

    IEnumerator EndFightCoroutine()
    {
        foreach (GameObject obj in barriers)
        {
            obj.GetComponent<Animator>().SetTrigger("stop");
        }
        musicAnim.Play("MusicFadeOut");

        yield return new WaitForSecondsRealtime(3.3f);
        DialogueManager.instance.PlayDialogue("DefeatCrab");
    }
}
