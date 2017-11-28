using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayerScript : MonoBehaviour {
    public GameObject player;
   // UnityEngine.Video.VideoPlayer video;
    bool fullScreen = true;
    private Vector3 initializePos;
    // Use this for initialization
    void Start () {
        initializePos = transform.position;
        //video = player.GetComponent<UnityEngine.Video.VideoPlayer>();
        transform.localScale = new Vector3(0.4f, 0.4f, 1);
       // video.frame = 0;
    }
	
    private void OnMouseOver()
    {
       // video.Pause();
         //video.Play();
    }
    private void OnMouseDown()
    {
        if (fullScreen)
        {
            fullScreen = !fullScreen;
            transform.localScale = new Vector3(1.85f, 1.94f, 1);
            transform.position = new Vector3(0, -1f, -1); ;
           // video.Play();
        }
        else
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 1);

            transform.position = initializePos;
            fullScreen = !fullScreen;
         //   video.frame = 0;
           // video.Pause();
        }       
    }
    private void OnMouseExit()
    {
        
    }
}
