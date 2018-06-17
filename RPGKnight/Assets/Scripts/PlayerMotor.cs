using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {
    Transform target; // target to follow
    NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f; // stop when entering the radius of our interactable
        target = newTarget.transform;        
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0;
        target = null;
    }
}
