using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour {

    public GameObject areaObject;
    public int lastAgentHit;

	// Use this for initialization
	void Start ()
    {
        lastAgentHit = -1;
	}
}
