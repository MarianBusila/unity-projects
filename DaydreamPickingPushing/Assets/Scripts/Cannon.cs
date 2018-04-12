using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    private float _force = 20;

    protected Vector3 Velocity
    {
        get { return transform.localToWorldMatrix * Vector3.up * _force; }
    }

    public GameObject cannonballPrefab;
	
	public void Fire () {
        // create a cannonball at the current position and rotation
        GameObject ball = Instantiate(cannonballPrefab, transform.position, transform.rotation);

        // get the rigidbody physics component
        var cannonballRigidbody = ball.GetComponent<Rigidbody>();

        // apply the velocity
        cannonballRigidbody.AddForce(Velocity, ForceMode.VelocityChange);
	}
}
