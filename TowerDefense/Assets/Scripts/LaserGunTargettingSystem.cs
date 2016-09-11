using UnityEngine;
using System.Collections;

public class LaserGunTargettingSystem : TurretTargettingSystem {

    LineRenderer lineRenderer;
    AudioSource audioSource;
	// Use this for initialization
	override protected void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
        base.Start();
	}

    override protected void MaybeFire()
    {
        if (currentTarget != null)
        {
            EnemyHealth enemyHealth = currentTarget.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
                lineRenderer.SetPosition(1, new Vector3(0f, 0f, distanceToTarget - 0.5f));
            }


            if (Time.time > fireCooldown)
            {
                fireCooldown = Time.time + delayBetweenFire;
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(10);
                    if (enemyHealth.GetCurrentHealth() <= 0)
                    {
                        currentTarget = null;
                        SetCurrentTurretState(TurretState.Idle);
                        enemyGameObjects.Remove(enemyHealth.gameObject);
                        enemyHealth.Die();
                    }
                }
            }
        }
    }

    override protected void EngagedTarget()
    {
        lineRenderer.SetPosition(1, Vector3.zero);
        lineRenderer.enabled = true;
        audioSource.enabled = true;
        audioSource.Play();
    }
    override protected void DisengagedTarget()
    {
        lineRenderer.enabled = false;
        audioSource.enabled = false;
    }
}
