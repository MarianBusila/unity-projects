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
	// There is one brain in the scene so this should find our brain.
        brain = FindObjectOfType<Brain>();
        academy = FindObjectOfType<PushBlockAcademy>();
    }

    public override void InitializeAgent()
    {
        goalDetect = block.GetComponent<GoalDetect>();
        goalDetect.agent = this;
        rayPerception = GetComponent<RayPerception>();

        // cache the agent rigidbody
        agentRB = GetComponent<Rigidbody>();
        // cache the block rigidbody
        blockRB = block.GetComponent<Rigidbody>();

        areaBounds = ground.GetComponent<Collider>().bounds;
        groundRenderer = ground.GetComponent<Renderer>();

        // starting material
        groundMaterial = groundRenderer.material;
    }

    public override void CollectObservations()
    {
        float rayDistance = 12f;
        float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 110f, 70f};
        string[] detectableObjects = new string[] { "block", "goal", "wall"};
        // foreach ray angle a vector of size detectableObjects + 2 is created. For example if goal is hit by spherecase, than the vector will be [0, 1, 0, 0, distance].
        // if no object is hit, then [0, 0, 0, 1, 0]. Basically vector[3] is set to 1 if no object is hit, vector[4] is set to the distance when one object was hit and his index vector[ 0 - 2] is set to 1
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects, 1.5f, 0f));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // move the agent using the action
        MoveAgent(vectorAction);

    }

    public override void AgentReset()
    {
        int rotation = Random.Range(0, 4);
        float rotationAngle = rotation * 90;
        area.transform.Rotate(new Vector3(0f, rotationAngle, 0f));

        ResetBlock();
        transform.position = GetRandomSpawnPos();
        agentRB.velocity = Vector3.zero;
        agentRB.angularVelocity = Vector3.zero;
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

    public void IScoredAGoal()
    {
        AddReward(5f);
        Done();

        // swap ground material for a bit to indicate we scored
        StartCoroutine(GoalScoredSwapGroundMaterial(academy.goalScoredMaterial, 0.5f));
    }

    private IEnumerator GoalScoredSwapGroundMaterial(Material mat, float time)
    {
        groundRenderer.material = mat;
        yield return new WaitForSeconds(time);
        groundRenderer.material = groundMaterial;
    }

    private void ResetBlock()
    {
        // get a random position for the block
        block.transform.position = GetRandomSpawnPos();
        blockRB.velocity = Vector3.zero;
        blockRB.angularVelocity = Vector3.zero;
    }

    private Vector3 GetRandomSpawnPos()
    {
        bool foundNewSpawnLocation = false;
        Vector3 randomSpawnPos = Vector3.zero;
        while (foundNewSpawnLocation == false)
        {
            float randomPosX = Random.Range(-areaBounds.extents.x * academy.spawnAreaMarginMultiplier,
                                areaBounds.extents.x * academy.spawnAreaMarginMultiplier);

            float randomPosZ = Random.Range(-areaBounds.extents.z * academy.spawnAreaMarginMultiplier,
                                            areaBounds.extents.z * academy.spawnAreaMarginMultiplier);
            randomSpawnPos = ground.transform.position + new Vector3(randomPosX, 1f, randomPosZ);
            if (Physics.CheckBox(randomSpawnPos, new Vector3(2.5f, 0.01f, 2.5f)) == false)
            {
                foundNewSpawnLocation = true;
            }
        }
        return randomSpawnPos;
    }
}
