using UnityEngine;
using System.Collections;

public class Pistol : MonoBehaviour {
    public int damage = 50;
    public int ammo = 20;
    //Optional
    public float range = 50;

    private AudioSource myAudioSource;

    public AudioClip shootSound;

	// Use this for initialization
	void Start ()
    {
        myAudioSource = GetComponent<AudioSource>();       
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Fire1") && ammo > 0)
        {
            Shoot();
        }
	}

    void Shoot()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                //Damage the enemy
                Debug.Log("We hit an enemy!");
            }
        }
        myAudioSource.PlayOneShot(shootSound);
        ammo--;
    }
}
