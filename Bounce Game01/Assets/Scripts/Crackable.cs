using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackable : MonoBehaviour {

    private Rigidbody2D rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    rigid.bodyType = RigidbodyType2D.Dynamic;
        //    rigid.gravityScale = 1;
        //    rigid.AddRelativeForce(Vector2.up * 10, ForceMode2D.Impulse);
        //   // rigid.AddForceAtPosition(Vector2.up * 10, transform.position);
        //    StartCoroutine(isTimeToDead(3));

        //}
    }

}
