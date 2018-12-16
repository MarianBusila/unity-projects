using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class ReacherAgent : Agent {
    public GameObject upperArm;
    public GameObject lowerArm;
    public GameObject hand;
    public GameObject goal;

    private ReacherAcademy reacherAcademy;

    private Rigidbody upperArmRB;
    private Rigidbody lowerArmRB;

    private float goalSpeed;

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

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        
    }

    public override void AgentReset()
    {
        
    }
}
