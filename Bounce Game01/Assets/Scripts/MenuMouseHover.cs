using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMouseHover : MonoBehaviour {

    public Sprite[] sprites;
    SpriteRenderer spriterenderer;
    public AudioClip clip;
    public int level = 1;
    public bool loadFromLastLevel = false;
    private GameManager manager;
    public GameObject blobi;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<GameManager>();
        spriterenderer = GetComponent<SpriteRenderer>();
        spriterenderer.sprite = sprites[0];
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.playOnAwake = false;
        audio.loop = false;
    }
	
    private void OnMouseOver()
    {
        spriterenderer.sprite = sprites[1];
        if (blobi != null)
        {
            Vector3 pos = blobi.transform.position;
            pos.y = transform.position.y;
            blobi.transform.position = pos;
        }

        audio.Play();
    }
    private void OnMouseDown()
    {
        if (level == -1)
        {
            Application.Quit();
            return;
        }
        if (loadFromLastLevel)
        {
            int formerLevel = manager.lastLevel;
            manager.loadFromlastScene = true;
            manager.Loadlevel(formerLevel);
        }
        else
        {
            manager.loadFromlastScene = false;
            manager.Loadlevel(level);
        }
        
    }
    private void OnMouseExit()
    {
        spriterenderer.sprite = sprites[0];
    }
}
