using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SnakeController : MonoBehaviour {
    private DetectedPlane detectedPlane;

    public GameObject snakeHeadPrefab;
    private GameObject snakeInstance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPlane(DetectedPlane plane)
    {
        this.detectedPlane = detectedPlane;
        SpawnSnake();
    }

    private void SpawnSnake()
    {
        if (snakeInstance != null)
        {
            Destroy(snakeInstance);
        }

        Vector3 pos = detectedPlane.CenterPose.position;
        pos.y += 0.1f;

        // not anchored, it is rigidbody that is influenced by the physics engine
        snakeInstance = Instantiate(snakeHeadPrefab, pos, Quaternion.identity, transform);

        // pass the head to the slithering
        GetComponent<Slithering>().Head = snakeInstance.transform;
    }
}
