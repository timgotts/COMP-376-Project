using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public Transform target;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float cameraLerpTime;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //if (Mathf.Abs(target.position.y - transform.position.y) >= 5)
       // {
            Vector3 targetPos = new Vector3(transform.position.x + xOffset,
                target.position.y + yOffset,
                transform.position.z + zOffset);
            transform.position = Vector3.Lerp(transform.position, targetPos, cameraLerpTime);
       // }

	}
}
