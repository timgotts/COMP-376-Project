using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public BouncingBall player;
    private GameManager gameManager;
    private Text pauseText;
    private Text scoreText;
	private Text powerUpsText;
    private Text bestScore;
    public GameObject soundObject;
    private SoundSlider soundSlider;
    // private bool isPause = false;
    // Use this for initialization
    void Awake ()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        GameObject text = GameObject.Find("Pause");
		player = GameObject.FindObjectOfType<BouncingBall> ();
        GameObject score = GameObject.Find("Score");
        GameObject bS = GameObject.Find("BestScore");
		GameObject powerUps = GameObject.Find ("PowerUps");
        if (text)
        {
            pauseText = text.GetComponent<Text>();
        }
        if (score)
        {
            scoreText = score.GetComponent<Text>();
        }
        if (bS)
        {
            bestScore = bS.GetComponent<Text>();
            bestScore.text = gameManager.bestscore.ToString();
        }
		if (powerUps) {
			powerUpsText = powerUps.GetComponent<Text> ();
		}

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (scoreText)
        {
            scoreText.text = gameManager.Score.ToString();
        }
			
		if (powerUpsText) {
			powerUpsText.text = player.grappleShots.ToString();
			}

    }
    public void Pause()
    {
        gameManager.Pause(pauseText);
    }
    public void Loadlevel(int level)
    {
        gameManager.Score = 0;
        SceneManager.LoadScene(level);
    }
    public void Setting()
    {
        gameManager.Pause(pauseText);
        if (soundSlider == null && gameManager.isPause)
        {
            GameObject sound = Instantiate(soundObject, new Vector3(-8, 20, 0), Quaternion.identity);
            soundSlider = sound.GetComponent<SoundSlider>();
            soundSlider.transform.parent = GameObject.Find("Canvas").transform;
            RectTransform rect = soundSlider.GetComponent<RectTransform>();
            rect.localPosition = Vector3.zero;
        }
        else
        {
            Destroy(soundSlider.gameObject);
        }
}
}
