using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Camera cam;
    public LayerMask movementMask;

	// Use this for initialization
	void Start () {
        cam = Camera.main;	
	}
	
	// Update is called once per frame
	void Update () {
        // left mouse button
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            float maxDistance = 100;
            if(Physics.Raycast(ray, out hit, maxDistance, movementMask))
            {
                // move player to what we hit
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // stop focusing any objects
            }
        }
	}
}
