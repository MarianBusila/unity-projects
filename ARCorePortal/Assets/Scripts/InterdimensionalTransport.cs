using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InterdimensionalTransport : MonoBehaviour
{
    public Material[] materials;

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

    private void OnTriggerStay(Collider other)
    {
        if(other.name != "PortalCamera")
        {
            return;
        }

        int stencilTest;
        // we are outside of other world
        if (transform.position.z > other.transform.position.z)
        {
            Debug.Log("Outside of other world");
            SetMaterials(false);
        }
        else
        {
            Debug.Log("Inside of the other world");
            SetMaterials(true);
        }
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
