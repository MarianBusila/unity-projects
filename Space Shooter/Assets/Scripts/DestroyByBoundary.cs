using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
    {
        Debug.Log("Destroy bolt");
        Destroy(other.gameObject);
    }
}
