using UnityEngine;
using System.Collections;

public class EnemyPathRoute : MonoBehaviour {

    public Transform[] path;
    public float moveSpeed = 2.0f;
    private int currentTargetIndex = 0;
    private bool reachedEndTarget = false;
    public int damageAmount = 10;

	// Use this for initialization
	void Start () {
        transform.position = path[currentTargetIndex].position;
        currentTargetIndex++;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(!reachedEndTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentTargetIndex].position, moveSpeed * Time.deltaTime);
            float distanceToTarget = Vector3.Distance(transform.position, path[currentTargetIndex].position);

            //start turning a little earlier than reaching the target
            int targetToLookAtIndex = currentTargetIndex;
            if(distanceToTarget < 1 && currentTargetIndex < path.Length - 1)
            {
                targetToLookAtIndex = currentTargetIndex + 1;
            }
            //change enemy direction
            Vector3 targetDir = path[targetToLookAtIndex].position - transform.position;
            float step = moveSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            if (distanceToTarget < 0.05f)
            {
                currentTargetIndex++;                

                if (currentTargetIndex == path.Length)
                    EnemySuccess();
            }
        }
	}

    void EnemySuccess()
    {
        Debug.Log("Reached destination");
        reachedEndTarget = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<Health>().TakeDamage(damageAmount);
        }

        Destroy(gameObject);
    }
}
