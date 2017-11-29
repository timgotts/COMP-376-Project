using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    GameObject player;
    Animator animator;
    GameManager gameManager;

    bool isCheckPointTouched;
    public bool touchedLava;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isCheckPointTouched", isCheckPointTouched);
    }

    /// <summary>
    /// Checks if the collider caused a trigger.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isCheckPointTouched)
        {
            gameManager.AddScore(1);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            isCheckPointTouched = true;
            player.GetComponent<BouncingBall>().currentCheckpoint = gameObject;
        }
        
        if (col.gameObject.name.Contains("Lava"))
        {
            touchedLava = true;
        }

    }
}
