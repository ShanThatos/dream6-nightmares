using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndController : MonoBehaviour
{
    public GameObject videoDisplay;
    public GameObject credits;
    public Animator scrollAnim;
    public Animator scenesAnim;
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += VideoEnded;
        if (PlayerPrefs.GetInt("Failed", 0) == 1)
        {
            videoDisplay.SetActive(false);
            videoPlayer.gameObject.SetActive(false);
            credits.SetActive(true);
            scrollAnim.speed = 10f;
            scenesAnim.speed = 10f;
        }
        else
        {
            videoDisplay.SetActive(true);
            videoPlayer.gameObject.SetActive(true);
            credits.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void VideoEnded(UnityEngine.Video.VideoPlayer vp)
    {
        credits.SetActive(true);
        videoDisplay.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
    }
}
