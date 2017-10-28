using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombspawner : MonoBehaviour {
    public Sprite[] bombColors;
    public GameObject bomb;
	// Use this for initialization
	void Start ()
    {
        //Invoke("LaunchBombs", 1);
        InvokeRepeating("LaunchBombs", 0.05f, 2.2f);
    }
	
    void LaunchBombs ()
    {
        GameObject b = Instantiate(bomb, transform.position, Quaternion.identity);
        b.GetComponent<SpriteRenderer>().sprite = bombColors[Random.Range(0,5)];
        b.transform.parent = transform;
    }
}
