using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool isPause = false;
    private float soundVolume = 0.6f;
    private int score = 0;
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
    }
	
	//// Update is called once per frame
	//void Update ()
 //   {
		
	//}
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    public void AddScore (int s)
    {
        score += s;
    }
    public float Sound
    {
        get
        {
            return soundVolume;
        }
        set
        {
            soundVolume = value;
        }
    }
    public void Pause(Text text)
    {
        isPause = !isPause;
        if (text)
        {
            text.color = isPause ? Color.red : Color.white;
        }
        Time.timeScale = isPause ? 0 : 1;
        
    }
    public void Loadlevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
