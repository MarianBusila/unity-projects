using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    Camera cam;
    public LayerMask movementMask;
    PlayerMotor motor;

    public Interactable focus;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        // left mouse button
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            float maxDistance = 100;
            if(Physics.Raycast(ray, out hit, maxDistance, movementMask))
            {
                // move player to what we hit
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);

                // stop focusing any objects
                RemoveFocus();
            }
        }

        // right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            float maxDistance = 100;
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // if we did set it as our focus
                if (interactable != null)
                {
                    Debug.Log("We hit interactable " + hit.collider.name + " " + hit.point);
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}
