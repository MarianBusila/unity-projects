using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public float fireRate = 0.5f;
    float cooldown = 0f;
    public float damage = 25f;
    FXManager fxManager;

    void Start()
    {
        fxManager = GameObject.FindObjectOfType<FXManager>();
        if (fxManager == null)
            Debug.LogError("Could not find FXManager");
    }


	// Update is called once per frame
	void Update ()
    {
        cooldown -= Time.deltaTime;
	    if(Input.GetButton("Fire1"))
        {
            //player wants to shoot
            Fire();
        }
	}
    void Fire()
    {
        if (cooldown > 0)
            return;

        Debug.Log("Firing our gun");

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        if(FindClosestHitInfo(ray, out hitInfo))
        {
            Debug.Log("We hit: " + hitInfo.transform.name);
            //we could do a special effect at the hit location
            //DoRicochetEffetAt(hitInfo.point);

            Transform hitTransform = hitInfo.transform;
            Health h = hitTransform.GetComponent<Health>();          
            while (h == null  && hitTransform.parent)
            {
                hitTransform = hitTransform.parent;
                h = hitTransform.GetComponent<Health>();
            }

            if (h != null)
                //h.TakeDamage(damage);
                h.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBuffered, damage);

            if(fxManager != null)
            {
                fxManager.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, Camera.main.transform.position, hitInfo.point);
            }
        }
        else
        {
            //we did not hit anything, but let's do a visual FX anyway
            if (fxManager != null)
            {
                Vector3 point = Camera.main.transform.position + Camera.main.transform.forward * 100f;
                fxManager.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, Camera.main.transform.position, point);
            }
        }

       

        cooldown = fireRate;
    }

    bool FindClosestHitInfo(Ray ray, out RaycastHit hitInfo)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool bHit = false;
        float distance = 0f;
        hitInfo = new RaycastHit();

        foreach(RaycastHit hit in hits)
        {
            if(hit.transform != this.transform && (distance == 0f || hit.distance < distance))
            {
                //we have hit something that is
                //a) not us
                //b) the first thing we hit or least closer than the previous closest thing
                hitInfo = hit;
                distance = hit.distance;
                bHit = true;
            }
        }
        return bHit;
    }
}
