using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationType1 : MonoBehaviour {


    public float rotationsPerMinute = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, rotationsPerMinute * Time.deltaTime), Space.World);
    }
}
