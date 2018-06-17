using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    public Vector3 offsetTarget;
    public float pitch = 2f;
    private float currentZoom = 10;

	
	void LateUpdate () {
        // camera will follow the target
        transform.position = target.position - offsetTarget * currentZoom;

        // look at target
        transform.LookAt(target.position + Vector3.up * pitch);
	}
}
