using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlockAgent : Agent {

    public GameObject ground;

    public GameObject area;

    [HideInInspector]
    public Bounds areaBounds;

    PushBlockAcademy academy;

    // the goal to push the block to
    public GameObject goal;

    // the block to be pushed to the goal
    public GameObject block;
    
    GoalDetect goalDetect;

    Rigidbody blockRB;
    Rigidbody agentRB;
    Material groundMaterial;
    RayPerception rayPerception;

    // will change the material based on success/failure
    Renderer groundRenderer;

    private void Awake()
    {
        academy = FindObjectOfType<PushBlockAcademy>();
    }

    public override void InitializeAgent()
    {
        //cache the agent rigidbody
        agentRB = GetComponent<Rigidbody>();
    }

    public override void CollectObservations()
    {
        
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // move the agent using the action
        MoveAgent(vectorAction);
    }

    public override void AgentReset()
    {
        
    }

    private void MoveAgent(float[] vectorAction)
    {
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        int action = Mathf.FloorToInt(vectorAction[0]);

        switch(action)
        {
            case 0: //W
                dirToGo = transform.forward * 1f;
                break;
            case 1: //S
                dirToGo = transform.forward * -1f;
                break;
            case 2: //D
                rotateDir = transform.up * 1f;
                break;
            case 3: //A
                rotateDir = transform.up * -1f;
                break;
            case 4: //Q
                dirToGo = transform.right * -0.75f;
                break;
            case 5: //E
                dirToGo = transform.right * 0.75f;
                break;
        }

        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        agentRB.AddForce(dirToGo * academy.agentRunSpeed, ForceMode.VelocityChange);
    }
}
