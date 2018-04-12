using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    private float _force = 20;
    private const float RotationalVelocity = 50;

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

    public void RotateClockwise()
    {
        Rotate(0, RotationalVelocity);
    }

    public void RotateCounterClockwise()
    {
        Rotate(0, -RotationalVelocity);
    }

    private void Rotate(float x, float y)
    {
        Vector3 transformEulerAngles = transform.eulerAngles;
        transformEulerAngles.x += x * Time.deltaTime;
        transformEulerAngles.y += y * Time.deltaTime;
        transform.eulerAngles = transformEulerAngles;
    }
}
