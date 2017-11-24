using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    /// <summary>
    /// The movement speed of the enemy.
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// Determines if the enemy is moving left or right.
    /// </summary>
    public bool moveRight;

    /// <summary>
    /// Enemy's Rigidbody.
    /// </summary>
    Rigidbody2D rigidBody2d;

    /// <summary>
    /// Transform which checks the if on ground.
    /// </summary>
    public Transform groundCheck;

    /// <summary>
    /// The radius to check.
    /// </summary>
    float groundCheckRadius = 0.1f;

    /// <summary>
    /// Checks if it will fall.
    /// </summary>
    bool isAboutToFall;



    // Use this for initialization
    void Start ()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        CheckGrounded();

        if (!isAboutToFall)
        {
            moveRight = !moveRight;
        }
        
        if (moveRight)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            rigidBody2d.velocity = new Vector2(moveSpeed, rigidBody2d.velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            rigidBody2d.velocity = new Vector2(-moveSpeed, rigidBody2d.velocity.y);
        }
    }

    /// <summary>
    /// Checks if enemy is on ground in order to turn around.
    /// </summary>
    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheckRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                isAboutToFall = true;
                return;
            }
        }
        isAboutToFall = false;
    }
}
