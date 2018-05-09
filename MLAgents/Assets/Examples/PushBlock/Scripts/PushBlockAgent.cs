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
        
    }

    public override void CollectObservations()
    {
        
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        
    }

    public override void AgentReset()
    {
        
    }
}
