using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;


public class SceneController : MonoBehaviour {
    public GameObject trackedPlanePrefab;

	// Use this for initialization
	void Start () {
        QuitOnConnectionErrors();
	}
	
	// Update is called once per frame
	void Update () {
		// the session status must be Tracking in order to access the Frame
        if(Session.Status != SessionStatus.Tracking)
        {
            int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        ProcessNewPlanes();
	}

    void QuitOnConnectionErrors()
    {
        if(Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            StartCoroutine(CodelabUtils.ToastAndExit("Camera permission is needed to run this application.", 5));
        }
        else if(Session.Status.IsError())
        {
            StartCoroutine(CodelabUtils.ToastAndExit("ARCore encountered a problem connecting. Please restart the app.", 5));
        }
    }

    void ProcessNewPlanes()
    {
        List<DetectedPlane> planes = new List<DetectedPlane>();
        Session.GetTrackables(planes, TrackableQueryFilter.New);

        for(int i = 0; i < planes.Count; i++)
        {
            // instantiate a plane visualization prefab and set it to track new plane
            // the transform is set to the origin with an identity rotation since the mesh for our prefab is updated in Unity world coordinates
            GameObject planeObject = Instantiate(trackedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
            planeObject.GetComponent<TrackedPlaneController>().SetTrackedPlane(planes[i]);
        }
    }
}
