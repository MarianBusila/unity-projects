using UnityEngine;
using System.Collections;

public class MachineGunTargettingSystem : TurretTargettingSystem {

    GameObject muzzleFlashParticleSystem;
    AudioSource audioSource;
	// Use this for initialization
	override protected void Start () {
        muzzleFlashParticleSystem = transform.Find("MuzzleFlash").gameObject;
        audioSource = GetComponent<AudioSource>();
        base.Start();
	}

    override protected void MaybeFire()
    {
        if (Time.time > fireCooldown)
        {
            fireCooldown = Time.time + delayBetweenFire;
            if (currentTarget != null)
            {
                EnemyHealth enemyHealth = currentTarget.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(20);
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
        muzzleFlashParticleSystem.SetActive(true);
        audioSource.loop = true;
        audioSource.Play();
    }
    override protected void DisengagedTarget()
    {
        muzzleFlashParticleSystem.SetActive(false);
        audioSource.loop = false;
    }
}
