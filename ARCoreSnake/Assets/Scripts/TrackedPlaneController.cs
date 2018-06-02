using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class TrackedPlaneController : MonoBehaviour {

    private DetectedPlane detectedPlane;
    private PlaneRenderer planeRenderer;
    private List<Vector3> polygonVertices = new List<Vector3>();

    private void Awake()
    {
        planeRenderer = GetComponent<PlaneRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// if no plane yet, disable the renderer and return
        if(detectedPlane == null)
        {
            planeRenderer.EnablePlane(false);
            return;
        }

        // if this plane was subsumed by another plane, destroy this object, the other plane's display will render it
        if(detectedPlane.SubsumedBy != null)
        {
            Destroy(gameObject);
            return;
        }

        // if this plane is not valid or ARCore is not tracking, disable rendering
        if(detectedPlane.TrackingState != TrackingState.Tracking || Session.Status != SessionStatus.Tracking)
        {
            planeRenderer.EnablePlane(false);
            return;
        }

        // valid plane, so enable rendering andupdate polygon data if needed
        planeRenderer.EnablePlane(true);
        List<Vector3> newPolygonVertices = new List<Vector3>();
        detectedPlane.GetBoundaryPolygon(newPolygonVertices);

        if (!AreVerticesListsEqual(polygonVertices, newPolygonVertices))
        {
            polygonVertices.Clear();
            polygonVertices.AddRange(newPolygonVertices);
            planeRenderer.UpdateMeshWithCurrentTrackedPlane(detectedPlane.CenterPose.position, polygonVertices);
        }
	}

    bool AreVerticesListsEqual(List<Vector3> firstList, List<Vector3> secondList)
    {
        if (firstList.Count != secondList.Count)
        {
            return false;
        }

        for (int i = 0; i < firstList.Count; i++)
        {
            if (firstList[i] != secondList[i])
            {
                return false;
            }
        }

        return true;
    }

    public void SetTrackedPlane(DetectedPlane plane)
    {
        detectedPlane = plane;
        detectedPlane.GetBoundaryPolygon(polygonVertices);
        planeRenderer.Initialize();
        planeRenderer.UpdateMeshWithCurrentTrackedPlane(detectedPlane.CenterPose.position, polygonVertices);
    }
}
