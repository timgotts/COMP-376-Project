using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

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
    /// Transform which checks the if on ground.
    /// </summary>
    public Transform wallCheck;

    /// <summary>
    /// The radius to check.
    /// </summary>
    float wallCheckRadius = 0.1f;


    /// <summary>
    /// Checks if it will fall.
    /// </summary>
    bool isAboutToFall;

    /// <summary>
    /// Check is it will hit wall.
    /// </summary>
    bool isHittingWall;

    /// <summary>
    /// The red key, the Red spider carries
    /// </summary>
    public GameObject redKey;

    /// <summary>
    /// The key pickup sound.
    /// </summary>
    public AudioClip dropKeySound;

    /// <summary>
    /// The death sound.
    /// </summary>
    public AudioClip deathSound;

    /// <summary>
    /// The scale variable
    /// </summary>
    float scale;

    /// <summary>
    /// Determines if enemy is dead.
    /// </summary>
    public bool isDead = false;

    AudioSource audioSource;

    public ParticleSystem explodeParticle;


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameObject.name.Contains("KeySpider"))
        {
            scale = 2.1f;
        }
        else
        {
            scale = 1.5f;
        }
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if(gameObject.name.Contains("KeySpider"))
            {
                audioSource.PlayOneShot(dropKeySound);
                Instantiate(redKey, transform.position, transform.rotation);
            }

            Instantiate(explodeParticle, gameObject.transform.position, gameObject.transform.rotation);
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject);
        }
        
        CheckGrounded();

        isHittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius);

        if (!isAboutToFall || isHittingWall)
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            transform.localScale = new Vector3(-scale, scale, scale);
            rigidBody2d.velocity = new Vector2(moveSpeed, rigidBody2d.velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(scale, scale, scale);
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
