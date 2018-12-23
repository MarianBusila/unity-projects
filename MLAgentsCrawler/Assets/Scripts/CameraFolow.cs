﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour {

    public Transform target;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = gameObject.transform.position - target.position;	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z);
        gameObject.transform.position = newPosition;
		
	}
}
