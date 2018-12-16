using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class ReacherAgent : Agent {
    public GameObject upperArm;
    public GameObject lowerArm;
    public GameObject hand;
    public GameObject goal;

    private ReacherAcademy reacherAcademy;

    private Rigidbody upperArmRB;
    private Rigidbody lowerArmRB;

    private float goalSpeed;
    private float goalSize;
    private float goalDegree;

    public override void InitializeAgent()
    {
        upperArmRB = upperArm.GetComponent<Rigidbody>();
        lowerArmRB = lowerArm.GetComponent<Rigidbody>();

        reacherAcademy = GameObject.Find("Academy").GetComponent<ReacherAcademy>();
    }

    public override void CollectObservations()
    {
        AddVectorObs(upperArm.transform.localPosition);
        AddVectorObs(upperArm.transform.rotation);
        AddVectorObs(upperArmRB.angularVelocity);
        AddVectorObs(upperArmRB.velocity);

        AddVectorObs(lowerArm.transform.localPosition);
        AddVectorObs(lowerArm.transform.rotation);
        AddVectorObs(lowerArmRB.angularVelocity);
        AddVectorObs(lowerArmRB.velocity);

        AddVectorObs(goal.transform.localPosition);
        AddVectorObs(hand.transform.localPosition);

        AddVectorObs(goalSpeed);
    }

    // the agent's four actions correspond to the torques on each of the two joints
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        goalDegree += goalSpeed;
        UpdateGoalPosition();

        var torqueX = Mathf.Clamp(vectorAction[0], -1f, 1f) * 150f;
        var torqueZ = Mathf.Clamp(vectorAction[1], -1f, 1f) * 150f;
        upperArmRB.AddTorque(new Vector3(torqueX, 0f, torqueZ));

        torqueX = Mathf.Clamp(vectorAction[2], -1f, 1f) * 150f;
        torqueZ = Mathf.Clamp(vectorAction[3], -1f, 1f) * 150f;
        lowerArmRB.AddTorque(new Vector3(torqueX, 0f, torqueZ));

    }

    private void UpdateGoalPosition()
    {
        var radians = goalDegree * Mathf.PI / 180f;
        var goalX = 8f * Mathf.Cos(radians);
        var goalY = 8f * Mathf.Sin(radians);
        goal.transform.position = new Vector3(goalY, -1f, goalX) + transform.position;
    }

    public override void AgentReset()
    {
        
    }
}
