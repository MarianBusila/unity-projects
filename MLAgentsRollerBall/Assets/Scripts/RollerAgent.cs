using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerAgent : Agent {

    Rigidbody rigidbody;

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
}
