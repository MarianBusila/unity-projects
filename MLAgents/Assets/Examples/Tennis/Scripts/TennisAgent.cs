using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisAgent : Agent {

    public int score;
    public bool invertX;
    public float invertMult;
    public float yStartPosition = 3.5f;

    public GameObject tennisArea;
    public GameObject ball;

    private void Awake()
    {
        // There is one brain in the scene so this should find our brain.
        brain = FindObjectOfType<Brain>();
    }

    public override void CollectObservations()
    {
        // observations about the agent relative to the tennis area
        AddVectorObs(invertMult * (gameObject.transform.position.x - tennisArea.transform.position.x));
        AddVectorObs(gameObject.transform.position.y - tennisArea.transform.position.y);
        AddVectorObs(invertMult * gameObject.GetComponent<Rigidbody>().velocity.x);
        AddVectorObs(gameObject.GetComponent<Rigidbody>().velocity.y);

        // observations about the ball relative to the tennis area
        AddVectorObs(invertMult * (ball.transform.position.x - tennisArea.transform.position.x));
        AddVectorObs(ball.transform.position.y - tennisArea.transform.position.y);
        AddVectorObs(invertMult * ball.GetComponent<Rigidbody>().velocity.x);
        AddVectorObs(ball.GetComponent<Rigidbody>().velocity.y);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {        
        float moveX = 0.0f;
        float moveY = 0.0f;
        moveX = 0.25f * Mathf.Clamp(vectorAction[0], -1f, 1f) * invertMult;
        if (Mathf.Clamp(vectorAction[1], -1f, 1f) > 0f && gameObject.transform.position.y - tennisArea.transform.position.y < yStartPosition)
        {
            moveY = 0.5f;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, moveY * 12f, 0f);
        }

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(moveX * 50f, gameObject.GetComponent<Rigidbody>().velocity.y, 0f);
        Debug.Log("VelocityX: " + gameObject.GetComponent<Rigidbody>().velocity.x + ", VelocityY: " + gameObject.GetComponent<Rigidbody>().velocity.y);        
    }

    public override void AgentReset()
    {        
        if (invertX)
        {
            invertMult = -1f;
        }
        else
        {
            invertMult = 1f;
        }

        gameObject.transform.position = new Vector3(-invertMult * Random.Range(6, 8), yStartPosition, 0f) + transform.parent.transform.position;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }

}
