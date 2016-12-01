using UnityEngine;
using System.Collections;

public class SpaceshipController : MonoBehaviour {

    public float speed = 2.0f;
	// Use this for initialization
	void Start () {
	
	}
		
	void FixedUpdate () {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += moveDirection * Time.deltaTime * speed;
	}
}
