using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTiles : MonoBehaviour
{


    public bool isCollided = false;
    private Rigidbody2D rigid;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Collider2D collider;


    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isCollided)
        {
            rigid.velocity = new Vector2(0, -2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.gravityScale = 1;
            isCollided = true;
            Destroy(collider);

            StartCoroutine(isTimeToDead(5));

        }
    }
    private IEnumerator isTimeToDead(float time)
    {
        yield return new WaitForSeconds(time);
        collider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        rigid.velocity = Vector2.zero;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        isCollided = false;
    }
}
