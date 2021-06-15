using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class destroyVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource bgMusic;
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "intro.mp4");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            bgMusic.Play();
            game.SetActive(true);
            Destroy(this.gameObject);
        }
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {

            bgMusic.Play();
            game.SetActive(true);
            Destroy(this.gameObject);
        }
        
    }
}
