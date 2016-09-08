using UnityEngine;
using System.Collections;

public class MissileControl : MonoBehaviour {

    public GameObject target;
    public float speed = 2f;
    public GameObject missileExplosionPrefab;
    public GameObject missileGun;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);

            if(Vector3.Distance(target.transform.position, transform.position) < 0.2f)
            {
                //missile hit target
                GameObject explosionGO = Instantiate(missileExplosionPrefab, transform.position, Quaternion.identity) as GameObject;                
                ParticleSystem explosionParticleSystem = explosionGO.GetComponent<ParticleSystem>();
                float totalDuration = explosionParticleSystem.startLifetime + explosionParticleSystem.duration;
                Destroy(gameObject);
                Destroy(explosionGO, totalDuration);

                //reduce health
                EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(25);
                    if (enemyHealth.GetCurrentHealth() <= 0)
                    {
                        MissileGunTargettingSystem missileGunTS = missileGun.GetComponent<MissileGunTargettingSystem>();
                        missileGunTS.currentTarget = null;
                        missileGunTS.SetCurrentTurretState(MissileGunTargettingSystem.TurretState.Idle);
                        missileGunTS.enemyGameObjects.Remove(enemyHealth.gameObject);
                        enemyHealth.Die();
                    }
                }
                
            }
        }
	}
}
