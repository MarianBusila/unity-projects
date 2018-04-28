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
        foreach (var material in materials)
        {
            material.SetInt("_StencilTest", (int)CompareFunction.Equal);
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
            stencilTest = (int)CompareFunction.Equal;
        }
        else
        {
            Debug.Log("Inside of the other world");
            stencilTest = (int)CompareFunction.NotEqual;
        }

        foreach (var material in materials)
        {
            material.SetInt("_StencilTest", stencilTest);
        }
    }

    private void OnDestroy()
    {
        // after we end the game, we want to see our objects with that material inside Unity editor
        foreach (var material in materials)
        {
            material.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
