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
    public struct CheckWIn
    {
        public string level1;
        public string level2;
        public string level3;
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
        LoaFormerState();
    }
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        CreateXMLForWinCondition();
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
    public void LoaFormerState()
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

    private void CreateXMLForWinCondition()
    {
        if (!File.Exists("xmlWin.xml"))
        {
            CheckWIn gd;
            gd.level1 = "0";
            gd.level2 = "0";
            gd.level3 = "0";
            XmlSerializer ser = new XmlSerializer(typeof(CheckWIn));
            using (var writer = new StreamWriter(File.Open("xmlWin.xml", FileMode.Create)))
            {
                ser.Serialize(writer, gd);
            }
        }
    }
    public void UpdateXMLForWinCondition(int level)
    {
        //SceneManager.LoadScene(level + 1);
       // if (File.Exists("xmlWin.xml"))
        //{
            int l1 = 0;
            int l2 = 0;
            int l3 = 0;
            CheckWIn gd;
            XmlSerializer ser = new XmlSerializer(typeof(CheckWIn));
            using (var reader = new StreamReader(File.Open("xmlWin.xml", FileMode.Open)))
            {
                gd = (CheckWIn)ser.Deserialize(reader);
                l1 = int.Parse(gd.level1);
                l2 = int.Parse(gd.level2);
                l3 = int.Parse(gd.level3);
                switch (level)
                {
                    case 1:
                        {
                            gd.level1 = level.ToString();
                            break;
                        }
                    case 2:
                        {
                            gd.level2 = level.ToString();
                            break;
                        }
                    case 3:
                        {
                            gd.level3 = level.ToString();
                            break;
                        }
                }

                if (l1 != 0 && l2 != 0 && l3 != 0)
                {
                    SceneManager.LoadScene("Win");
                    if (File.Exists("xmlWin.xml"))
                    {
                        File.Delete("xmlWin.xml");
                    }
                }
                else
                {
                //XmlSerializer ser2 = new XmlSerializer(typeof(CheckWIn));
                //using (var writer = new StreamWriter(File.Open("xmlWin.xml", FileMode.Create)))
                //{
                //    ser2.Serialize(writer, gd);
                //}
               // Debug.Log("next level");
               if (level == 4)
                {
                    SceneManager.LoadScene("Win");
                }
               else
                {
                    SceneManager.LoadScene(level + 1);
                }
                    
                }

            }
       // }
    }
}
