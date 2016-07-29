using UnityEngine;
using System.Collections;

public class Enemy_Chase : MonoBehaviour {

    private NavMeshAgent myAgent;
    public Transform transformTarget;

	// Use this for initialization
	void Start () {
        myAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        myAgent.SetDestination(transformTarget.position);
	
	}
}
