using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChain : MonoBehaviour {

    public float distance = 3;
    public float speed = 1;
    private Rigidbody2D rigid;
    private Vector3 startPos;
    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        //rigid.AddForce(Vector2.left * force);
        // StartCoroutine(Swing(10));
        startPos = transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 v = startPos;
        v.x += distance * Mathf.Sin(Time.time * speed);
        transform.position = v;
        //transform.position = new Vector3(Mathf.PingPong(Time.time * speed, distance), transform.position.y, transform.position.z);
    }

//private IEnumerator Swing(float time)
//    {
//        yield return new WaitForSeconds(time);
//        Debug.Log("force");
//        rigid.AddForce(Vector2.left * force);
//    }
}
