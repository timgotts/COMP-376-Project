using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public int level = 1;
    public string key01;
    public string key02;
    public float speed;
    public float force = 7;
    public float transformForce = 3;
    public float JumpSpeed = 200;

    public bool isSwinging;

    public GameObject rockPowerUpContainer;
    public GameObject ropePowerUpContainer;
    public bool canOverJump = false;
    public GameObject bouncebox;

    public GameObject camera;
    private Rigidbody2D rigid;

    AudioSource audioSource;
    public AudioClip bounceSound;
    Animator animator;
    bool hasBounced = false;
    private GameManager gameManager;
    private bool isConenctedToRope = false;
    private bool rotateCamera = false;
    public bool rotateCameraLeft = true;
    private float rotationsPerMinute = -1.0f;
    float mIdleTime = 10.0f;
    float mTimer = 0.0f;
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


    // This struct will be used for chaking the number of bounce in the level 1
    public struct BounecBox
    {
        public float timeOfDead;
        public bool isAlive;
        public GameObject boxCircle;
        public Vector3 pos;
    }
    private BounecBox[] BounceBoxesLevel1;

    // Use this for initialization
    void Start()
    {

        // Set the camera rotation zero
        if (camera != null)
        {
           camera.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        // Set the color to green
        GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        tempVec = gameObject.transform.position;
        tempVec.y -= 0.44f;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        audioSource.Play();

        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager.gameIsLoaded)
        {
                this.transform.position = gameManager.InitialPos();
        }
        // Set the position if last level is loaded
        if (gameManager.loadFromlastScene)
        {
            transform.position = gameManager.lastPos;
        }

        // initialize the bounce boxe
        if (level == 1)
        {
            BounceBoxesLevel1 = new BounecBox[6];
            for (int i = 0; i < 6; ++i)
            {
                GameObject bonce = GameObject.Find("Box_Circle_" + i);
                BounceBoxesLevel1[i] = new BounecBox { timeOfDead = 0f, isAlive = true, boxCircle = bonce, pos = bonce.transform.position };
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       

        // Check bounce boxes in level 1
        if (level == 1)
        {
            CheckNumberOfBoxCircles();
            // Rotate camera
            if (rotateCamera && camera != null && !canOverJump)
            {
                if (rotateCameraLeft)
                {
                    //float zRotation = Mathf.PingPong(Time.time * rotationsPerMinute, 90f);
                    Quaternion newRotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
                    camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, newRotation, .0005f);
                    //Debug.Log("z: " + camera.transform.rotation.eulerAngles.z);
                    if (camera.transform.rotation.eulerAngles.z >= 89)
                    {
                        //Debug.Log("z positive: " + camera.transform.rotation.z);
                        rotateCameraLeft = false;
                    }
                }
                else
                {
                    //float zRotation = Mathf.PingPong(Time.time * rotationsPerMinute, 90f);
                    Quaternion newRotation = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
                    camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, newRotation, .0005f);
                    if (camera.transform.rotation.eulerAngles.z >= 269)
                    {
                        rotateCameraLeft = true;
                    }
                }

                GameObject.Find("Image_Compass").GetComponent<Image>().GetComponent<Image>().color = new Color32(255, 255, 225, 225);
                GameObject.Find("Arrow_Compass").GetComponent<Image>().GetComponent<Image>().color = new Color32(255, 255, 225, 225);
            }
            else
            {
                GameObject.Find("Image_Compass").GetComponent<Image>().GetComponent<Image>().color = new Color32(255, 255, 225, 0);
                GameObject.Find("Arrow_Compass").GetComponent<Image>().GetComponent<Image>().color = new Color32(255, 255, 225, 0);
            }
        }

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
          if (audioSource.volume != gameManager.Sound)
        {
                  audioSource.volume = gameManager.Sound;
        }



        animator.SetBool("hasBounced", hasBounced);
        animator.SetBool("inRockMode", inRockMode);

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
        CheckTimeForOverJump();
        if (Input.GetKeyDown(KeyCode.J) && canOverJump)
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
            SaveLevelInfo();
            Respawn();
            // SceneManager.LoadScene("Loose");

        }
        else if (collision.gameObject.tag == "Bomb")
        {
            //Debug.Log("Losse by bomb");
            //gameManager.Save(transform.position.y.ToString());
            //Time.timeScale = 0;
            SaveLevelInfo();
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
                SaveLevelInfo();
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
        // Rotate the camera
        if (collision.gameObject.name.Contains("RotateCamera"))
        {
            rotateCamera = true;
        }
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

        if (collision.gameObject.CompareTag("WinPortal"))
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void CheckTimeForOverJump()
    {
        if (canOverJump)
        {
            // Set the camera rotation zero
            if (camera != null)
            {
                camera.transform.localEulerAngles = new Vector3(0, 0, 0);
                rotateCamera = false;
            }
            mTimer += Time.deltaTime;
            if (mTimer >= mIdleTime)
            {
                canOverJump = false;
                mTimer = 0;
                // Set the color to Green
               GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            }
        }
    }

    private void CheckNumberOfBoxCircles ()
    {
        for (int i = 0; i < 6; ++i)
        {
            GameObject bonce = GameObject.Find("Box_Circle_" + i);
            if (bonce == null)
            {
                BounceBoxesLevel1[i].timeOfDead += Time.deltaTime;
                if (BounceBoxesLevel1[i].timeOfDead >= 4)
                {
                    BounceBoxesLevel1[i].timeOfDead = 0;
                    bonce = Instantiate(bouncebox, BounceBoxesLevel1[i].pos, Quaternion.identity);
                    bonce.name = "Box_Circle_" + i.ToString();
                }               
            }
        }
    }

    private void SaveLevelInfo()
    {
        GameManager.LevelScore l;
        l.pozX = currentCheckpoint.transform.position.x.ToString();
        l.pozY = currentCheckpoint.transform.position.y.ToString();
        l.pozZ = currentCheckpoint.transform.position.z.ToString();
        l.score = gameManager.Score.ToString();
        l.level = level.ToString();
        gameManager.Save(l);
    }
}
