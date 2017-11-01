using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class GameManager : MonoBehaviour
{
    public struct LevelScore
    {
        public string height;
        public string score;
    };
    public bool isPause = false;
    private float soundVolume = 0.6f;
    private int score = 0;
    public int bestscore = 0;
    public float lastPos = 0;
    public bool gameIsLoaded = false;
    // Use this for initialization
    private void Awake()
    {
        LoadBestScore();
    }
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

    public void Save(LevelScore gd)
    {
        if (score > bestscore)
        {
            bestscore = score;
        }
        XmlSerializer ser = new XmlSerializer(typeof(LevelScore));
        using (var writer = new StreamWriter(File.Open("xmlsave.xml", FileMode.Create)))
        {
            if (score > bestscore)
            {
                gd.score = score.ToString();
            }
            else
            {
                gd.score = bestscore.ToString();
            }
            ser.Serialize(writer, gd);
        }
    }
    public void Load()
    {
        gameIsLoaded = true;
        string gd;
        XmlSerializer ser = new XmlSerializer(typeof(string));
        using (var reader = new StreamReader(File.Open("xmlsave.xml", FileMode.Open)))
        {
            gd = ser.Deserialize(reader) as string;
            lastPos = float.Parse(gd);
            SceneManager.LoadScene(1);
        }
    }
    public void LoadBestScore()
    {
        LevelScore gd;
        XmlSerializer ser = new XmlSerializer(typeof(LevelScore));
        using (var reader = new StreamReader(File.Open("xmlsave.xml", FileMode.Open)))
        {
            gd = (LevelScore)ser.Deserialize(reader);
            // lastPos = float.Parse(LevelScore);
            bestscore =int.Parse (gd.score);
        }
    }
    public Vector3 InitialPos()
    {
        gameIsLoaded = false;
        int y = (int)lastPos;
        if (y <= 5)
        {
            return new Vector3(1.58f, -0.91f, 0);
        }
        else if (y > 5 && y <= 20)
        {
            return new Vector3(5.24f, 9.09f, 0);
        }
        else if (y > 20 && y <= 48)
        {
            return new Vector3(-5.49f, 24.11f, 0);
        }
        else if (y > 50 && y <= 80)
        {
            return new Vector3(4.25f, 52.02f, 0);
        }
        else if (y > 80)
        {
            return new Vector3(7.44f, 82.28f, 0);
        }
        else
        {
            return new Vector3(1.58f, -0.91f, 0);
        }

    }
}
