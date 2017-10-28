using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCircle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(isTimeToDead(0.1f));

        }
    }
    private IEnumerator isTimeToDead(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
