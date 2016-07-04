using UnityEngine;
using System.Collections;

public class FlipperScript : MonoBehaviour {

    bool isActive = false;
    HingeJoint myJoint;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().maxAngularVelocity = Mathf.Infinity;
        myJoint = GetComponent<HingeJoint>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftControl) == true && isActive == false)
        {
            isActive = true;
            JointSpring spring = myJoint.spring;
            spring.targetPosition = -25;
            myJoint.spring = spring;
        }
        else
            if (Input.GetKey(KeyCode.LeftControl) == false && isActive == true)
        {
            isActive = false;
            JointSpring spring = myJoint.spring;
            spring.targetPosition = 15;
            myJoint.spring = spring;
        }
	}

}
