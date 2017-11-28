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
            BouncingBall blobi = collision.gameObject.GetComponent<BouncingBall>();
            // Set the color to Red
            blobi.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            blobi.canOverJump = true;
            StartCoroutine(isTimeToDead(0.1f));

        }
    }
    private IEnumerator isTimeToDead(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
