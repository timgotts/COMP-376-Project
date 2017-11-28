using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    GameObject player;
    Animator animator;

    bool isCheckPointTouched;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
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
        if (col.gameObject.CompareTag("Player"))
        {
            isCheckPointTouched = true;
            player.GetComponent<BouncingBall>().currentCheckpoint = gameObject;
        }
    }
}
