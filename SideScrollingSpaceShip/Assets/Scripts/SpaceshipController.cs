using UnityEngine;
using System.Collections;

public class SpaceshipController : MonoBehaviour {

    public float speed = 2.0f;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
	
	}
		
	void FixedUpdate () {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 proposedPosition = transform.position + moveDirection * Time.deltaTime * speed;
        transform.position = new Vector3(Mathf.Clamp(proposedPosition.x, -1.6f, 1.6f), Mathf.Clamp(proposedPosition.y, 0.4f, 3.6f), proposedPosition.z);

	}

    void OnTriggerEnter(Collider collider)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        ScoreController.isAlive = false;
        Destroy(gameObject);
    }
}
