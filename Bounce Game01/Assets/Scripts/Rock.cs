using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public ParticleSystem glowParticle;
    public ParticleSystem glowTemp;
    //ParticleSystem glowParticleSystem;

    float originalY;
    float floatStrength = 0.2f;
    // Use this for initialization
    void Start()
    {
       // glowParticleSystem = glowParticle.GetComponent<>
        this.originalY = gameObject.transform.position.y;
        glowTemp = Instantiate(glowParticle, transform.position, transform.rotation);
        glowTemp.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x ,(originalY + ((float)Math.Sin(Time.time) * floatStrength)), transform.position.z);
        //glowParticle.transform.position = transform.position;
    }
    
}
