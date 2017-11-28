using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    Animator animator;
    AudioSource audioSource;
    bool hasStarted = true;
    public GameObject level1;
    public GameObject credits;

    public AudioClip selectSound;

    bool onLevel1;
    bool onCredits;

    Vector3 vecPos1;
    Vector3 vecPos2;

    bool locker = false;
    Stopwatch stopWatch;

    bool playOnce = true;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        vecPos1 = level1.transform.position;
        vecPos1.x -= 4.5f;
       // gameObject.transform.position = vecPos1;
        onLevel1 = true;

        vecPos2 = credits.transform.position;
        vecPos2.x -= 4.5f;
        stopWatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("hasStarted", hasStarted);

        if (locker && stopWatch.ElapsedMilliseconds > 3000)
        {

            if (onLevel1)
            {
                SceneManager.LoadScene(1);
            }
            else if (onCredits)
            {
                //SceneManager.LoadScene(2);
            }
        }

        if (Input.GetButton("Up"))
        {
            if (!locker)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(selectSound);
                }
                onLevel1 = true;
                onCredits = false;
            }
        }
        else if (Input.GetButton("Down"))
        {
            if (!locker)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(selectSound);
                }
                onLevel1 = false;
                onCredits = true;
            }
        }

        if (onLevel1)
        {
            //gameObject.transform.position = vecPos1;
            level1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            credits.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (onCredits)
        {
            //gameObject.transform.position = vecPos2;
            credits.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            level1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if (Input.GetButton("Submit"))
        {
            //if (playOnce)
            //{
            SceneManager.LoadScene("Level01");
            //startMenu.GetComponent<AudioSource>().Stop();
            // audioSource.Stop();
            //audioSource.PlayOneShot(readySound);
            // isSelected = true;
            // stopWatch.Start();
            locker = true;
            playOnce = false;
            //}

        }
    }
}
