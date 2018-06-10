using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System;

public class ScoreboardController : MonoBehaviour {
    public Camera firstPersonCamera;
    private Anchor anchor;
    private DetectedPlane detectedPlane;
    private float yOffset;
    private int score;


	// Use this for initialization
	void Start () {
		foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		// the tracking state must be Tracking to access the Frame
        if(Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // if there is no plane, then return
        if(detectedPlane == null)
        {
            return;
        }

        // check the plane being subsumed. If the plane has ben subsumed switch attachment to the subsuming plane
        while(detectedPlane.SubsumedBy != null)
        {
            detectedPlane = detectedPlane.SubsumedBy;
        }

        // make the scoreboard face the viewer
        transform.LookAt(firstPersonCamera.transform);

        // move the position to stay consistent with the plane
        transform.position = new Vector3(transform.position.x, detectedPlane.CenterPose.position.y + yOffset, transform.position.z);
	}

    public void SetSelectedPlane(DetectedPlane detectedPlane)
    {
        this.detectedPlane = detectedPlane;
        CreateAnchor();
    }

    public void SetScore(int score)
    {
        if(this.score != score)
        {
            GetComponentInChildren<TextMesh>().text = "Score: " + score;
            this.score = score;
        }
    }

    private void CreateAnchor()
    {
        // create the position of the anchor by raycasting a point towards the top of the screen
        Vector2 pos = new Vector2(Screen.width * .5f, Screen.height * .9f);
        Ray ray = firstPersonCamera.ScreenPointToRay(pos);
        Vector3 anchorPosition = ray.GetPoint(5f);

        //create the anchor at that point
        if(anchor != null)
        {
            DestroyObject(anchor);
        }
        anchor = detectedPlane.CreateAnchor(new Pose(anchorPosition, Quaternion.identity));

        // attach the scoreboard to the anchor
        transform.position = anchorPosition;
        transform.SetParent(anchor.transform);

        // record the y offset from the plane
        yOffset = transform.position.y - detectedPlane.CenterPose.position.y;

        // enable the renderers
        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = true;

        }
    }
}
