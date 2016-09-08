using UnityEngine;
using System.Collections;

public class MissileGunTargettingSystem : TurretTargettingSystem {

    AudioSource audioSource;
    public GameObject missileGameObject;
	// Use this for initialization
	override protected void Start () {
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
                //Instantiate missile
                GameObject missile = Instantiate(missileGameObject, transform.position + transform.forward, Quaternion.identity) as GameObject;
                missile.GetComponent<MissileControl>().target = currentTarget;
                missile.GetComponent<MissileControl>().missileGun = gameObject;                              
            }
        }
    }

    override protected void EngagedTarget()
    {        
        audioSource.loop = true;
        audioSource.Play();
    }
    override protected void DisengagedTarget()
    {
        audioSource.loop = false;
    }
}
