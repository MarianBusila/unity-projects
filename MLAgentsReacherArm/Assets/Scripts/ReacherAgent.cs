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

    // resets the position and velocity of the agent and the goal
    public override void AgentReset()
    {
        upperArm.transform.position = new Vector3(0f, -4f, 0f) + transform.position;
        upperArm.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        upperArmRB.angularVelocity = Vector3.zero;
        upperArmRB.velocity = Vector3.zero;

        lowerArm.transform.position = new Vector3(0f, -10f, 0f) + transform.position;
        lowerArm.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        lowerArmRB.angularVelocity = Vector3.zero;
        lowerArmRB.velocity = Vector3.zero;

        goalDegree = Random.Range(0f, 360f);
        UpdateGoalPosition();

        goalSize = reacherAcademy.goalSize;
        goalSpeed = Random.Range(-1f, 1f) * reacherAcademy.goalSpeed;
        goal.transform.localScale = new Vector3(goalSize, goalSize, goalSize);
    }
}
