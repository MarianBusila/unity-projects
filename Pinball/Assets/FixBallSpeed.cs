using UnityEngine;
using System.Collections;

public class FixBallSpeed : MonoBehaviour {

    Rigidbody myRigidBody;
	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody>();
        myRigidBody.maxAngularVelocity = Mathf.Infinity;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        myRigidBody.WakeUp();
    }
}
