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
        public string level;
        public string pozX;
        public string pozY;
        public string pozZ;
        public string score;
    };
    public bool loadFromlastScene = false; 
    public bool isPause = false;
    private float soundVolume = 0.6f;
    private int score = 0;
    public int bestscore = 0;
    public int lastLevel = 1;
    public Vector3 lastPos;
    public bool gameIsLoaded = false;
    // Use this for initialization
    private void Awake()
    {
        LoadBestScore();
    }
    void Start ()
    {
        //Did this for testing
        //SceneManager.LoadScene("Level02");
        DontDestroyOnLoad(gameObject);        
    }
	
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
    public void Loadlevel1()
    {
        SceneManager.LoadScene("Level01");
    }
    public void Loadlevel2()
    {
        SceneManager.LoadScene("Level02");
    }


    public void Save(LevelScore gd)
    {
        Debug.Log("from game manager: " + gd.pozY);
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
        LevelScore gd;
        XmlSerializer ser = new XmlSerializer(typeof(LevelScore));
        using (var reader = new StreamReader(File.Open("xmlsave.xml", FileMode.Open)))
        {
            gd = (LevelScore)ser.Deserialize(reader);
            bestscore = int.Parse(gd.score);
            lastPos.x = float.Parse(gd.pozX);
            lastPos.y = float.Parse(gd.pozY);
            lastPos.z = float.Parse(gd.pozZ);
            lastLevel = int.Parse(gd.level);
            SceneManager.LoadScene(lastLevel);
        }      
    }
    public void LoadBestScore()
    {
        if (File.Exists("xmlsave.xml"))
        {
            LevelScore gd;
            XmlSerializer ser = new XmlSerializer(typeof(LevelScore));
            using (var reader = new StreamReader(File.Open("xmlsave.xml", FileMode.Open)))
            {
                gd = (LevelScore)ser.Deserialize(reader);
                bestscore = int.Parse(gd.score);
                lastPos.x = float.Parse(gd.pozX);
                lastPos.y = float.Parse(gd.pozY);
                lastPos.z = float.Parse(gd.pozZ);
                lastLevel = int.Parse(gd.level);
            }
        }

    }
    public Vector3 InitialPos()
    {
        gameIsLoaded = false;
        return lastPos;
    }
    public void Loadlevel(int lev)
    {
        SceneManager.LoadScene(lev);
    }
}
