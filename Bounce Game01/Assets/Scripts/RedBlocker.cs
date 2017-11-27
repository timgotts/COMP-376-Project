using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlocker : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip unlockSound;
    bool hasPlayedSound = false;

    bool doOnce = true;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print("in red");

        if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<BouncingBall>().hasRedKey)
        {
            if (!hasPlayedSound)
            {
                audioSource.PlayOneShot(unlockSound);
                hasPlayedSound = true;
            }

            if (doOnce)
            {
                print("in player");
               
                foreach (Transform child in transform)
                {
                    Rigidbody2D rididbody2d = child.gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
                    Vector2 exp = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                    rididbody2d.AddForce(exp * 10, ForceMode2D.Impulse);
                    Destroy(child.gameObject.GetComponent<Collider2D>());
                }
                doOnce = false;

                StartCoroutine(isTimeToDead(this.gameObject, 5));
            }
        }
    }

    private IEnumerator isTimeToDead(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

}
