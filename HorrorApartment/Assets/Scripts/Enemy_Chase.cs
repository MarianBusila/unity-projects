using UnityEngine;
using System.Collections;

public class Enemy_Chase : MonoBehaviour {

    private NavMeshAgent myAgent;
    private Animator myAnimator;
    public Transform transformTarget;

    public bool chaseTarget = true;
    public float stoppingDistance = 2.5f;
    public float delayBetweenAttacks = 1.5f;
    private float attackCooldown;

    private float distanceFromTarget;

	// Use this for initialization
	void Start () {
        myAgent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        myAgent.stoppingDistance = stoppingDistance;
        attackCooldown = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ChaseTarget();
	}

    void ChaseTarget()
    {
        distanceFromTarget = Vector3.Distance(transformTarget.position, transform.position);
        if(distanceFromTarget >= stoppingDistance)
        {
            chaseTarget = true;
        }
        else
        {
            chaseTarget = false;
            Attack();
        }

        if (chaseTarget)
        {
            myAgent.SetDestination(transformTarget.position);
            myAnimator.SetBool("isChasing", true);
        }
        else
        {
            myAnimator.SetBool("isChasing", false);
        }
    }

    void Attack()
    {
        if(Time.time > attackCooldown)
        {
            Debug.Log("Attack");
            myAnimator.SetTrigger("Attack");
            attackCooldown = Time.time + delayBetweenAttacks;
        }
    }
}
