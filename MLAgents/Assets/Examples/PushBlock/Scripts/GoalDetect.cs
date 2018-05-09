using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetect : MonoBehaviour {

    [HideInInspector]
    // the associated agent
    public PushBlockAgent agent;

    void OnCollisionEnter(Collision col)
    {
        // touched gol
        if(col.gameObject.CompareTag("goal"))
        {
            agent.IScoredAGoal();
        }
    }
}
