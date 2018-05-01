using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerAgent : Agent {

    Rigidbody rigidbody;
    float previousDistance = float.MaxValue;

    public float speed = 2.0f;
    public Transform Target;
    

    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    public override void AgentReset()
    {
        if(this.transform.position.y < -1.0)
        {
            // the agent fell
            this.transform.position = Vector3.zero;
            this.rigidbody.angularVelocity = Vector3.zero;
            this.rigidbody.velocity = Vector3.zero;
        }
        else
        {
            // move the target to a new spot between [-4, 4] coordinates on x and z
            this.Target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
        }
    }

    public override void CollectObservations()
    {
        /* Position of the target. In general, it is better to use the relative position of other objects rather than the absolute position for more generalizable training. 
         Note that the agent only collects the x and z coordinates since the floor is aligned with the x-z plane and the y component of the target's position never changes */
        Vector3 relativePosition = Target.position - this.transform.position;
        AddVectorObs(relativePosition.x / 5);
        AddVectorObs(relativePosition.z / 5);

        // Position of the agent itself within the confines of the floor. This data is collected as the agent's distance from each edge of the floor.
        AddVectorObs((this.transform.position.x + 5) / 5);
        AddVectorObs((this.transform.position.x - 5) / 5);
        AddVectorObs((this.transform.position.z + 5) / 5);
        AddVectorObs((this.transform.position.z - 5) / 5);

        // the velocity of the agent.This helps the agent learn to control its speed so it doesn't overshoot the target and roll off the platform.
        AddVectorObs(rigidbody.velocity.x / 5);
        AddVectorObs(rigidbody.velocity.z / 5);
    }

    // AgentAction receives the decision from the Brain
    // The first element,action[0] determines the force applied along the x axis; action[1] determines the force applied along the z axis
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);

        // reached target
        if(distanceToTarget < 1.42f)
        {
            Done();
            AddReward(1.0f);
        }

        // getting closer
        if (distanceToTarget < previousDistance)
        {
            AddReward(0.1f);
        }

        // time penalty
        AddReward(-0.05f);

        if(this.transform.position.y < -1.0f)
        {
            Done();
            AddReward(-1.0f);
        }

        previousDistance = distanceToTarget;

        /* The agent clamps the action values to the range [-1,1] for two reasons. First, the learning algorithm has less incentive to try very large values 
         * (since there won't be any affect on agent behavior), which can avoid numeric instability in the neural network calculations. Second, nothing prevents 
         * the neural network from returning excessively large values, so we want to limit them to reasonable ranges in any case. */
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = Mathf.Clamp(vectorAction[0], -1, 1);
        controlSignal.z = Mathf.Clamp(vectorAction[1], -1, 1);
        rigidbody.AddForce(controlSignal * speed);
    }
}
