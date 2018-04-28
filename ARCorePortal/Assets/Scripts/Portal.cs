using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour
{
    public Material[] materials;

    public Transform device;

    bool wasInFront;
    bool inOtherWorld;

	// Use this for initialization
	void Start () {
        // we are outside at the beginning
        SetMaterials(false);
    }

    void SetMaterials(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;
        foreach (var material in materials)
        {
            material.SetInt("_StencilTest", (int)stencilTest);
        }
    }

    bool GetIsInFront()
    {
        Vector3 pos = transform.InverseTransformPoint(device.position);
        return pos.z >= 0 ? true : false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != device)
        {
            return;
        }

        wasInFront = GetIsInFront();
        Debug.Log("OnTriggerEnter. wasInFront: " + wasInFront);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform != device)
        {
            return;
        }

        bool isInFront = GetIsInFront();
        Debug.Log("OnTriggerStay. isInFront:" + isInFront + ", wasInFront" + wasInFront);
        if ((isInFront && !wasInFront) || (wasInFront && !isInFront))
        {
            inOtherWorld = !inOtherWorld;
            SetMaterials(inOtherWorld);
        }

        wasInFront = isInFront;
    }

    private void OnDestroy()
    {
        // after we end the game, we want to see our objects with that material inside Unity editor
        SetMaterials(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
