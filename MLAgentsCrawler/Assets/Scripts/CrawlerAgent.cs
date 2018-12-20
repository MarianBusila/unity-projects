using UnityEngine;
using MLAgents;

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
