using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundSlider : MonoBehaviour {

    public Slider volumeSlider;
    private GameManager gameManager;
    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        volumeSlider.value = gameManager.Sound;
    }
    private void Update()
    {
        gameManager.Sound = volumeSlider.value;
    }
}
