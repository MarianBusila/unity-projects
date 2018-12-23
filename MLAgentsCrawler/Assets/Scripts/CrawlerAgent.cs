using UnityEngine;
using MLAgents;

[RequireComponent(typeof(JointDriveController))]
public class CrawlerAgent : Agent {
    [Header("Target to walk towards")]
    [Space(10)]
    public Transform target;
    public Transform ground;

    public bool detectTargets;
    public bool respawnTargetWhenTouched;
    public float targetSpawnRadius;

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
    float movingTowardsDot;
    float facingDot;

    [Header("Reward Functions To Use")]
    [Space(10)]
    public bool rewardMovingTowardsTarget; // Agent should move towards target
    public bool rewardFacingTarget; // Agent should face the target
    public bool rewardUseTimePenalty; // Hurry up


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

    /// <summary>
    /// Add relevant information on each body part to observations.
    /// </summary>
    public void CollectObservationBodyPart(BodyPart bp)
    {
        var rb = bp.rb;
        AddVectorObs(bp.groundContact.touchingGround ? 1 : 0); // Whether the bp touching the ground
        AddVectorObs(rb.velocity);
        AddVectorObs(rb.angularVelocity);

        if (bp.rb.transform != body)
        {
            Vector3 localPosRelToBody = body.InverseTransformPoint(rb.position);
            AddVectorObs(localPosRelToBody);
            AddVectorObs(bp.currentXNormalizedRot); // Current x rot
            AddVectorObs(bp.currentYNormalizedRot); // Current y rot
            AddVectorObs(bp.currentZNormalizedRot); // Current z rot
            AddVectorObs(bp.currentStrength / jdController.maxJointForceLimit);
        }
    }

    public override void CollectObservations()
    {
        jdController.GetCurrentJointForces();
        // Normalize dir vector to help generalize
        AddVectorObs(dirToTarget.normalized);

        // Forward & up to help with orientation
        AddVectorObs(body.transform.position.y);
        AddVectorObs(body.forward);
        AddVectorObs(body.up);
        foreach (var bodyPart in jdController.bodyPartsDict.Values)
        {
            CollectObservationBodyPart(bodyPart);
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (detectTargets)
        {
            foreach (var bodyPart in jdController.bodyPartsDict.Values)
            {
                if (bodyPart.targetContact && !IsDone() && bodyPart.targetContact.touchingTarget)
                {
                    TouchedTarget();
                }
            }
        }

        // Update pos to target
        dirToTarget = target.position - jdController.bodyPartsDict[body].rb.position;

        // The dictionary with all the body parts in it are in the jdController
        var bpDict = jdController.bodyPartsDict;

        int i = -1;
        // Pick a new target joint rotation
        bpDict[leg0Upper].SetJointTargetRotation(vectorAction[++i], vectorAction[++i], 0);
        bpDict[leg1Upper].SetJointTargetRotation(vectorAction[++i], vectorAction[++i], 0);
        bpDict[leg2Upper].SetJointTargetRotation(vectorAction[++i], vectorAction[++i], 0);
        bpDict[leg3Upper].SetJointTargetRotation(vectorAction[++i], vectorAction[++i], 0);
        bpDict[leg0Lower].SetJointTargetRotation(vectorAction[++i], 0, 0);
        bpDict[leg1Lower].SetJointTargetRotation(vectorAction[++i], 0, 0);
        bpDict[leg2Lower].SetJointTargetRotation(vectorAction[++i], 0, 0);
        bpDict[leg3Lower].SetJointTargetRotation(vectorAction[++i], 0, 0);

        // Update joint strength
        bpDict[leg0Upper].SetJointStrength(vectorAction[++i]);
        bpDict[leg1Upper].SetJointStrength(vectorAction[++i]);
        bpDict[leg2Upper].SetJointStrength(vectorAction[++i]);
        bpDict[leg3Upper].SetJointStrength(vectorAction[++i]);
        bpDict[leg0Lower].SetJointStrength(vectorAction[++i]);
        bpDict[leg1Lower].SetJointStrength(vectorAction[++i]);
        bpDict[leg2Lower].SetJointStrength(vectorAction[++i]);
        bpDict[leg3Lower].SetJointStrength(vectorAction[++i]);

        // Set reward for this step according to mixture of the following elements.
        if (rewardMovingTowardsTarget)
        {
            RewardFunctionMovingTowards();
        }

        if (rewardFacingTarget)
        {
            RewardFunctionFacingTarget();
        }

        if (rewardUseTimePenalty)
        {
            RewardFunctionTimePenalty();
        }
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

    /// <summary>
    /// Agent touched the target
    /// </summary>
    public void TouchedTarget()
    {
        AddReward(1f);
        if (respawnTargetWhenTouched)
        {
            GetRandomTargetPos();
        }
    }

    /// <summary>
    /// Moves target to a random position within specified radius.
    /// </summary>
    public void GetRandomTargetPos()
    {
        Vector3 newTargetPos = Random.insideUnitSphere * targetSpawnRadius;
        newTargetPos.y = 5;
        target.position = newTargetPos + ground.position;
    }

    /// <summary>
    /// Reward moving towards target & Penalize moving away from target.
    /// </summary>
    void RewardFunctionMovingTowards()
    {
        movingTowardsDot = Vector3.Dot(jdController.bodyPartsDict[body].rb.velocity, dirToTarget.normalized);
        AddReward(0.03f * movingTowardsDot);
    }

    /// <summary>
    /// Reward facing target & Penalize facing away from target
    /// </summary>
    void RewardFunctionFacingTarget()
    {
        facingDot = Vector3.Dot(dirToTarget.normalized, body.forward);
        AddReward(0.01f * facingDot);
    }

    /// <summary>
    /// Existential penalty for time-contrained tasks.
    /// </summary>
    void RewardFunctionTimePenalty()
    {
        AddReward(-0.001f);
    }

}
