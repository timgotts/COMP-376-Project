using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrackable : MonoBehaviour {

    public GameObject Object;
    public Transform transform;
    public int numberOfInstant;
    private GameManager gameManager;
    // Use this for initialization
    void Start () {
        CreateDuplicate(numberOfInstant);
        //gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameManager.AddScore (1);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            foreach (Transform child in this.transform)
            {
                Crackable obj = child.gameObject.GetComponent<Crackable>();
                Rigidbody2D rigid = child.gameObject.GetComponent<Rigidbody2D>();
                rigid.bodyType = RigidbodyType2D.Dynamic;
                rigid.gravityScale = 1;
                Vector2 exp = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                rigid.AddRelativeForce(exp * 10, ForceMode2D.Impulse);

            }
            StartCoroutine(isTimeToDead(this.gameObject, 2));


        }
    }
    void CreateDuplicate (int num)
    {
        for (int i =0; i < num; ++i)
        {
            Vector3 newpos =new Vector3(transform.position.x + Object.transform.localScale.x * i, transform.position.y, transform.position.z);
            GameObject obj1= Instantiate(Object, newpos, Quaternion.identity);
            obj1.transform.parent = this.transform;
            Vector3 newpos2 = new Vector3(transform.position.x + Object.transform.localScale.x * i, transform.position.y + Object.transform.localScale.y , transform.position.z);
            GameObject obj2 = Instantiate(Object, newpos2, Quaternion.identity);
            obj2.transform.parent = this.transform;
        }
    }


    private IEnumerator isTimeToDead(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
