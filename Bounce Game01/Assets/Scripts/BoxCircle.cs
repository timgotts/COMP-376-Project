using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCircle : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BouncingBall blobi = collision.gameObject.GetComponent<BouncingBall>();
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
