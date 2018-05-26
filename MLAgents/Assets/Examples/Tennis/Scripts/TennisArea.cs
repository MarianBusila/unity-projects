using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisArea : MonoBehaviour {

    public GameObject ball;
    public GameObject agentA;
    public GameObject agentB;

    void Start()
    {
        MatchReset();
    }

    public void MatchReset()
    {
        float ballResetXPosition = Random.Range(6f, 8f);
        float ballResetYPosition = 6f;
        int flip = Random.Range(0, 2);

        if(flip == 0)
        {
            // ball should be in the agentA's zone
            ball.transform.position = new Vector3(-ballResetXPosition, ballResetYPosition, 0f) + transform.position;
        }
        else
        {
            // ball should be in the agentB's zone
            ball.transform.position = new Vector3(ballResetXPosition, ballResetYPosition, 0f) + transform.position;
        }

        ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        // ball.GetComponent<HitWall>().lastAgentHit = -1;
    }

    private void FixedUpdate()
    {
        // clamp velocity
        float clampVelocity = 9f;
        Vector3 rbVelocity = ball.GetComponent<Rigidbody>().velocity;
        ball.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(rbVelocity.x, -clampVelocity, clampVelocity), Mathf.Clamp(rbVelocity.y, -clampVelocity, clampVelocity), rbVelocity.z);
    }
}
