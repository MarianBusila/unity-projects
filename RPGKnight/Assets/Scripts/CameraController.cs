using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    public Vector3 offsetTarget;

    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float yawSpeed = 100f;

    public float pitch = 2f;

    private float currentZoom = 10;
    private float currentYaw;

    private void Update()
    {
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        }
    }

    void LateUpdate () {
        // camera will follow the target
        transform.position = target.position - offsetTarget * currentZoom;

        // look at target
        transform.LookAt(target.position + Vector3.up * pitch);

        // rotate camera
        transform.RotateAround(target.position, Vector3.up, currentYaw);
	}
}
