using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YelloType : MonoBehaviour
{

    public bool isCollided = false;
    private Rigidbody2D rigid;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
            isCollided = true;
            StartCoroutine(isTimeToDead(3));

        }
    }
    private IEnumerator isTimeToDead(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
