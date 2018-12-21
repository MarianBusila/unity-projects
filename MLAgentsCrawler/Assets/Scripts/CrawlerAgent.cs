using UnityEngine;
using MLAgents;

[RequireComponent(typeof(JointDriveController))]
public class CrawlerAgent : Agent {
    [Header("Target to walk towards")]
    [Space(10)]
    public Transform target;
    public Transform ground;

    [Header("Body parts")]
    [Space(10)]
    public Transform body;
    public Transform leg0Upper;
    public Transform leg0Lower;
    public Transform leg1Upper;
    public Transform leg1Lower;
    public Transform leg2Upper;
    public Transform leg2Lower;
    public Transform leg3Upper;
    public Transform leg3Lower;

    [Header("Joint settings")]
    [Space(10)]
    JointDriveController jdController;
    Vector3 dirToTarget;


    public override void InitializeAgent()
    {
        jdController = GetComponent<JointDriveController>();

        //Setup each body part
        jdController.SetupBodyPart(body);
        jdController.SetupBodyPart(leg0Upper);
        jdController.SetupBodyPart(leg0Lower);
        jdController.SetupBodyPart(leg1Upper);
        jdController.SetupBodyPart(leg1Lower);
        jdController.SetupBodyPart(leg2Upper);
        jdController.SetupBodyPart(leg2Lower);
        jdController.SetupBodyPart(leg3Upper);
        jdController.SetupBodyPart(leg3Lower);
    }

    public override void CollectObservations()
    {
        
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        
    }

    public override void AgentReset()
    {
        if(dirToTarget != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dirToTarget);
        }

        foreach(var bodyPart in jdController.bodyPartsDict.Values)
        {
            bodyPart.Reset(bodyPart);
        }
    }
}
