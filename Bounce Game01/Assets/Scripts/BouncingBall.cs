using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BouncingBall : MonoBehaviour {

    public string key01;
    public string key02;
    public float speed;
    public float force = 10;
    public float transformForce = 3;
    public float JumpSpeed = 200;
    private Rigidbody2D rigid;
    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis(key01) == 1)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            rigid.AddForce(Vector3.right * transformForce, ForceMode2D.Force);
        }
        if (Input.GetAxis(key02) == 1)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            rigid.AddForce(Vector3.left * transformForce, ForceMode2D.Force);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
            rigid.velocity = new Vector2(0,0);
            this.transform.parent = null;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Jump");
            rigid.AddForce(Vector3.up * JumpSpeed, ForceMode2D.Force);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ColliderType1")
        {
            rigid.AddForce(Vector3.up * force, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.tag == "ColliderType2")
        {
            //rigid.AddForceAtPosition(Vector3.up * force * 15.0f, transform.position, ForceMode2D.Impulse);
           // Vector3 reflect = Vector3.Reflect(transform.position, Vector3.right);
            rigid.AddForce(Vector3.up * force , ForceMode2D.Impulse);
            //rigid.AddForce(reflect * Time.deltaTime * 5, ForceMode2D.Impulse);
        }

        else if (collision.gameObject.GetComponent<YelloType>())
        {
            
            //rigid.AddForceAtPosition(Vector3.up * force, transform.position, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            if (collision.gameObject.GetComponent<YelloType>().isCollided)
            {
                rigid.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            }
                 
        }
        else if (collision.gameObject.tag == "LooseBorder" || collision.gameObject.tag == "Bomb")
        {
            SceneManager.LoadScene("Loose");

        }
        else if (collision.gameObject.tag == "Hook" && Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Hook");
            rigid.bodyType = RigidbodyType2D.Kinematic;
            this.transform.parent = collision.gameObject.transform;

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftRotateCollider")
        {
            rigid.velocity = Vector3.zero;
            Debug.Log("leftcollider");
            rigid.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
        }
        //Debug.Log("leftcollider");
    }
}
