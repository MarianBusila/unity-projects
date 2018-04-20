using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    private float _force = 20;
    private const float RotationalVelocity = 50;
    private LineRenderer _lineRenderer;

    protected Vector3 Velocity
    {
        get { return transform.localToWorldMatrix * Vector3.up * _force; }
    }

    public GameObject cannonballPrefab;

    void Start()
    {
        // get the line renderer for the trajectory simulation
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        const int numberOfPositionsToSimulate = 50;
        const float timestampBetweenPositions = 0.2f;

        // setup the initial conditions
        Vector3 simulatePosition = transform.position;
        Vector3 simulatedVelocity = Velocity;

        // update the position count
        _lineRenderer.positionCount = numberOfPositionsToSimulate;

        for(int i = 0; i < numberOfPositionsToSimulate; i++)
        {
            // set each position of the line renderer
            _lineRenderer.SetPosition(i, simulatePosition);

            // change the velocity based on Gravity and the time step
            simulatedVelocity += Physics.gravity * timestampBetweenPositions;

            // change the position based on Gravity and the time step
            simulatePosition += simulatedVelocity * timestampBetweenPositions;
        }
    }

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

    public void RotateUp()
    {
        Rotate(-RotationalVelocity, 0);
    }

    public void RotateDown()
    {
        Rotate(RotationalVelocity, 0);
    }

    private void Rotate(float x, float y)
    {
        Vector3 transformEulerAngles = transform.eulerAngles;
        transformEulerAngles.x += x * Time.deltaTime;
        transformEulerAngles.y += y * Time.deltaTime;
        transform.eulerAngles = transformEulerAngles;
    }
}
