using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour {

    Rigidbody2D rigidBody2D;

    // Use this for initialization
    void Start ()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime *0.5f, Space.World);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        //Game over if player touches lava
        if (col.gameObject.CompareTag("Player"))
        {
            print("Lava death");
            SceneManager.LoadScene("Loose");
        }
    }

}
