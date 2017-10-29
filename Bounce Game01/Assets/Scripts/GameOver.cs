using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
