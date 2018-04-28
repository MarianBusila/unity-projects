using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float yaw = 0f;
    private float speed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // movement on X/Z
        transform.Translate(speed * Input.GetAxis("Horizontal"), 0, speed * Input.GetAxis("Vertical"));

        // rotation
        yaw += Input.GetAxis("Mouse X");        
        transform.eulerAngles = new Vector3(0f, yaw, 0f);
    }
}
