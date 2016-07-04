using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
 
        if (anim != null)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0f || Mathf.Abs(Input.GetAxis("Vertical")) > 0f)
            {
                anim.SetFloat("Speed", 1.0f);
            }
            else
            {
                anim.SetFloat("Speed", 0.0f);
            }
        }
    }
}
