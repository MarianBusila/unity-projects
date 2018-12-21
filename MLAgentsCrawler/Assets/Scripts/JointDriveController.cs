using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BodyPart
{
    [Header("Body part info")]
    public ConfigurableJoint joint;
    public Rigidbody rb;
    public Vector3 startingPos;
    public Quaternion startingRot;

    [HideInInspector] public JointDriveController thisJDController;

    /// <summary>
    /// Reset body part to initial configuration.
    /// </summary>
    public void Reset(BodyPart bp)
    {
        bp.rb.transform.position = bp.startingPos;
        bp.rb.transform.rotation = bp.startingRot;
        bp.rb.velocity = Vector3.zero;
        bp.rb.angularVelocity = Vector3.zero;
    }
}

public class JointDriveController : MonoBehaviour
{

    [HideInInspector] public Dictionary<Transform, BodyPart> bodyPartsDict = new Dictionary<Transform, BodyPart>();

    [HideInInspector] public List<BodyPart> bodyPartsList = new List<BodyPart>();

    /// <summary>
    /// Create BodyPart object and add it to dictionary.
    /// </summary>
    public void SetupBodyPart(Transform t)
    {
        BodyPart bp = new BodyPart
        {
            rb = t.GetComponent<Rigidbody>(),
            joint = t.GetComponent<ConfigurableJoint>(),
            startingPos = t.position,
            startingRot = t.rotation
        };
        bp.rb.maxAngularVelocity = 100;

        bp.thisJDController = this;
        bodyPartsDict.Add(t, bp);
        bodyPartsList.Add(bp);
    }
}
