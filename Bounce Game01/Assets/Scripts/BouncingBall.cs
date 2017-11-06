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

    public GameObject rockPowerUp;
    private Rigidbody2D rigid;

    AudioSource audioSource;
    public AudioClip bounceSound;
    Animator animator;
    bool hasBounced = false;
    private GameManager gameManager;
    private bool isConenctedToRope = false;

    bool inRockMode = false;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        audioSource.Play();

        gameManager = GameObject.FindObjectOfType<GameManager>();
        
        if (gameManager.gameIsLoaded)
        {
            this.transform.position = gameManager.InitialPos();
        }

    }

    // Update is called once per frame
    void Update()
    {
        // keep rotation 0 alwayes
        if (transform.rotation.z != 0)
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

        Vector3 velocity = new Vector3(0, rigid.velocity.y, 0);
        rigid.velocity = velocity;

        if (Input.GetAxis(key01) == 1 && !isConenctedToRope)
        {
            if (inRockMode)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                transform.Rotate(Vector3.forward,  Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                rigid.AddForce(Vector3.right * transformForce, ForceMode2D.Force);
            }
        }
        if (Input.GetAxis(key02) == 1 && !isConenctedToRope)
        {
            if (inRockMode)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                transform.Rotate(Vector3.back, 2.0f * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                rigid.AddForce(Vector3.left * transformForce, ForceMode2D.Force);
            }
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
            rigid.AddForce(Vector3.up * JumpSpeed, ForceMode2D.Force);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasBounced = true;
        audioSource.PlayOneShot(bounceSound);

        if (collision.gameObject.tag == "ColliderType1")
        {
            if (!inRockMode)
            {
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            }
           
        }
        else if (collision.gameObject.tag == "ColliderType2")
        {
            if (!inRockMode)
            {
                rigid.velocity = Vector3.zero;
                rigid.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            }

            //rigid.AddForceAtPosition(Vector3.up * force * 15.0f, transform.position, ForceMode2D.Impulse);
            // Vector3 reflect = Vector3.Reflect(transform.position, Vector3.right);

            //rigid.AddForce(reflect * Time.deltaTime * 5, ForceMode2D.Impulse);
        }

        else if (collision.gameObject.GetComponent<YelloType>())
        {
            if (!inRockMode)
            {
                //rigid.AddForceAtPosition(Vector3.up * force, transform.position, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                if (collision.gameObject.GetComponent<YelloType>().isCollided)
                {
                    rigid.velocity = Vector3.zero;
                    rigid.AddForce(Vector3.up * force, ForceMode2D.Impulse);
                }
            }
        }
        else if (collision.gameObject.tag == "LooseBorder")
        {
            Debug.Log("Losse by border");
            GameManager.LevelScore l;
            l.height = transform.position.y.ToString();
            l.score = gameManager.Score.ToString();
            gameManager.Save(l);
            SceneManager.LoadScene("Loose");

        }
        else if (collision.gameObject.tag == "Bomb")
        {
            Debug.Log("Losse by bomb");
            //gameManager.Save(transform.position.y.ToString());
            //Time.timeScale = 0;
            SceneManager.LoadScene("Loose");

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

        if (collision.gameObject.CompareTag("Rock"))
        {
            print("Rock collision");
            Destroy(rockPowerUp);
            inRockMode = true;

            //Set Physics Material to nothing
            gameObject.GetComponent<CircleCollider2D>().sharedMaterial = null;
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        hasBounced = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftRotateCollider")
        {
            rigid.velocity = Vector3.zero;
            Debug.Log("leftcollider");
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
        }
        //Debug.Log("leftcollider");
    }
}
