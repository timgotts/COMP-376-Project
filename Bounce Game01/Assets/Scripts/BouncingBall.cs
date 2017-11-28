using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{

    public string key01;
    public string key02;
    public float speed;
    public float force = 7;
    public float transformForce = 3;
    public float JumpSpeed = 200;
    public bool isSwinging;

    public GameObject rockPowerUpContainer;
    public GameObject ropePowerUpContainer;
    private Rigidbody2D rigid;

    AudioSource audioSource;
    public AudioClip bounceSound;
    Animator animator;
    bool hasBounced = false;
    //private GameManager gameManager;
    private bool isConenctedToRope = false;


    /// <summary>
    /// All the audio clips
    /// </summary>
    public AudioClip rockExplosion;
    public AudioClip powerUpLost;


    //Tarik rock stuff
    public GameObject windAnimation;
    bool inRockMode = false;
    public bool inRopeMode = false;
    bool hasJumped = false;
    bool hasGroundPound = false;

    public Transform groundCheck;
    Vector3 tempVec;
    bool isGrounded = false;
    float groundCheckRadius = 0.5f;

    public GameObject respawnParticle;

    bool isDead = false;

    public GameObject currentCheckpoint;

    public PhysicsMaterial2D bounciness;

    public AudioClip keyPickUpSound;
    public bool hasRedKey = false;

    public Vector2 ropeHook;
    public float swingForce = 4f;
    public GrapplingHook hook;


    // Use this for initialization
    void Start()
    {

        tempVec = gameObject.transform.position;
        tempVec.y -= 0.44f;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        audioSource.Play();

        //gameManager = GameObject.FindObjectOfType<GameManager>();

       // if (gameManager.gameIsLoaded)
        {
        //        this.transform.position = gameManager.InitialPos();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {

        }

        //Checks if Blobby is grounded.
        CheckGrounded();

        tempVec = gameObject.transform.position;
        tempVec.y -= 0.44f;

        groundCheck.position = tempVec;

        //Rotate the skybox
        //RenderSettings.skybox.SetFloat("_Rotation", Time.time * 4);

        // keep rotation 0 alwayes
        if (transform.rotation.z != 0 && !inRockMode)
        {
            transform.rotation = Quaternion.identity;
        }
        // Adjust volume
        //  if (audioSource.volume != gameManager.Sound)
        {
         //         audioSource.volume = gameManager.Sound;
        }



        animator.SetBool("hasBounced", hasBounced);
        animator.SetBool("inRockMode", inRockMode);

        
        //Vector3 velocity = new Vector3(0, rigid.velocity.y, 0);
        //rigid.velocity = velocity;


        if (!isSwinging)
        {
            if (Input.GetAxis(key01) == 1 && !isConenctedToRope && !hasGroundPound)
            {
                if (inRockMode)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);

                    transform.Rotate(Vector3.back * 10, Space.World);
                }
                else
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                    rigid.AddForce(Vector3.right * transformForce, ForceMode2D.Force);
                }
            }
            else if (Input.GetAxis(key02) == 1 && !isConenctedToRope && !hasGroundPound)
            {
                if (inRockMode)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
                    transform.Rotate(Vector3.forward * 10, Space.World);
                }
                else
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                    rigid.AddForce(Vector3.left * transformForce, ForceMode2D.Force);
                }
            }

            else
            {
                if (rigid.velocity.x < 0)
                {
                    rigid.velocity = new Vector2(Mathf.Max(rigid.velocity.x + 0.1f, 0), rigid.velocity.y);
                }
                else if (rigid.velocity.x > 0)
                {
                    rigid.velocity = new Vector2(Mathf.Min(0, rigid.velocity.x - 0.1f), rigid.velocity.y);
                }
            }
        }
       


        if (Input.GetButtonDown("Jump") && isGrounded && inRockMode)
        {
            transform.rotation = Quaternion.identity;
            rigid.AddForce(Vector3.up * JumpSpeed * 1.5f, ForceMode2D.Force);
        }
        else if (Input.GetButtonDown("Down") && !isGrounded && inRockMode)
        {
            hasGroundPound = true;
            transform.rotation = Quaternion.identity;
            GameObject temp = Instantiate(windAnimation, groundCheck.position, groundCheck.rotation);
            temp.GetComponent<Animator>().SetBool("isReady", true);
            temp.transform.parent = gameObject.transform;

            rigid.AddForce(Vector3.down * 15, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GiveUpPower();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.velocity = new Vector2(0, 0);
            this.transform.parent = null;
            isConenctedToRope = false;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            rigid.AddForce(Vector3.up * JumpSpeed / 2, ForceMode2D.Force);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasBounced = true;

        if (!inRockMode)
        {
            //Only play bounce sound when in blob mode
            audioSource.PlayOneShot(bounceSound);

            if (collision.gameObject.tag == "ColliderType1")
            {
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            }
            else if (collision.gameObject.tag == "ColliderType2")
            {
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * force, ForceMode2D.Impulse);


                //rigid.AddForceAtPosition(Vector3.up * force * 15.0f, transform.position, ForceMode2D.Impulse);
                // Vector3 reflect = Vector3.Reflect(transform.position, Vector3.right);

                //rigid.AddForce(reflect * Time.deltaTime * 5, ForceMode2D.Impulse);
            }

            else if (collision.gameObject.GetComponent<YelloType>())
            {
                //rigid.AddForceAtPosition(Vector3.up * force, transform.position, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                if (collision.gameObject.GetComponent<YelloType>().isCollided)
                {
                    rigid.velocity = Vector3.zero;
                    rigid.AddForce(Vector3.up * force, ForceMode2D.Impulse);
                }
            }
            else if(collision.gameObject.tag == "SandTile")
            {
                print("Sand Tile col");
                
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * 7.5f, ForceMode2D.Impulse);
            }

        }

        if (collision.gameObject.tag == "LooseBorder")
        {
            //Debug.Log("Losse yby border");
             //GameManager.LevelScore l;
            // l.height = transform.position.y.ToString();
            // l.score = gameManager.Score.ToString();
            // gameManager.Save(l);

            Respawn();
            // SceneManager.LoadScene("Loose");

        }
        else if (collision.gameObject.tag == "Bomb")
        {
            //Debug.Log("Losse by bomb");
            //gameManager.Save(transform.position.y.ToString());
            //Time.timeScale = 0;

            Respawn();
            //SceneManager.LoadScene("Loose");

        }
        else if (collision.gameObject.tag == "Hook" && Input.GetKey(KeyCode.A))
        {
            if (!inRockMode)
            {
                isConenctedToRope = true;
                rigid.bodyType = RigidbodyType2D.Kinematic;
                this.transform.parent = collision.gameObject.transform;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasGroundPound)
            {
                audioSource.PlayOneShot(rockExplosion);
                collision.gameObject.GetComponent<EnemyMove>().isDead = true;
            }
            else if (inRockMode)
            {
                GiveUpPower();
            }
            else
            {
                Respawn();
                //SceneManager.LoadScene("Loose");
            }
        }
    }


    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            if (isSwinging)
            {
                // 1 - Get a normalized direction vector from the player to the hook point
                var playerToHookDirection = (ropeHook - (Vector2) transform.position).normalized;

                // 2 - Inverse the direction to get a perpendicular direction
                Vector2 perpendicularDirection;
                if (horizontalInput > 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2) transform.position - perpendicularDirection * -2f;
                    //Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2) transform.position + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                rigid.AddForce(force, ForceMode2D.Force);
            }

        }
    }

    /// <summary>
    /// Respawns player at checkpoint
    /// </summary>
    void Respawn()
    {
        if (hook != null)
        {
            hook.ResetRope();
        }
        transform.position = currentCheckpoint.transform.position;
        
        Instantiate(respawnParticle, gameObject.transform.position, gameObject.transform.rotation);

    }

    /// <summary>
    /// Checks if Blobby is on ground to prevnt double jumping.
    /// </summary>
    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject && !col.gameObject.CompareTag("Enemy"))
            {
                GameObject temp = GameObject.Find("WindAnimation(Clone)");
                Destroy(temp);
                isGrounded = true;

                if (hasGroundPound)
                {
                    audioSource.PlayOneShot(rockExplosion);
                }

                hasGroundPound = false;
                return;
            }
        }
        isGrounded = false;
    }

    void SetRockMode()
    {
        inRockMode = true;

        //Turn bounciness off
        gameObject.GetComponent<CircleCollider2D>().sharedMaterial = null;
        rigid.gravityScale = 3;
    }


    void SetRopeMode()
    {
        inRopeMode = true;

        
    }
    void setBlobMode()
    {
        if (inRockMode)
        {
            inRockMode = false;
            rigid.gravityScale = 1;

            //Turn Bounciness off.
            rigid.AddForce(Vector3.up * 0.5f, ForceMode2D.Impulse);

            //Turn bounciness on.
            gameObject.GetComponent<CircleCollider2D>().sharedMaterial = bounciness;
            foreach (Transform rockChild in rockPowerUpContainer.transform)
            {
                rockChild.gameObject.SetActive(true);
            }

        }
        if (inRopeMode)
        {
            inRopeMode = false;
            hook.ResetRope();

            foreach (Transform ropeChild in ropePowerUpContainer.transform)
            {
                ropeChild.gameObject.SetActive(true);
            }
        }








        


    }

    /// <summary>
    /// Gives up the power up obtained
    /// </summary>
    private void GiveUpPower()
    {
        audioSource.PlayOneShot(powerUpLost);
        setBlobMode();
    }

    /// <summary>
    /// Sets has bounced to false in order to play the 
    /// appropriate animation
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        hasBounced = false;
    }

    /// <summary>
    /// Checks if the collider caused a trigger.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftRotateCollider")
        {
            rigid.velocity = Vector3.zero;
            Debug.Log("leftcollider");
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            SetRockMode();
            rigid.gravityScale = 3;

            foreach (Transform rockChild in rockPowerUpContainer.transform)
            {
                rockChild.gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.CompareTag("Rope"))
        {
            SetRopeMode();

            foreach (Transform ropeChild in ropePowerUpContainer.transform)
            {
                ropeChild.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("RedKey"))
        {
            audioSource.PlayOneShot(keyPickUpSound);
            hasRedKey = true;
            Destroy(collision.gameObject);
        }
    }
}
