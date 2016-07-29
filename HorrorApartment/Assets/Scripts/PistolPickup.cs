using UnityEngine;
using System.Collections;

public class PistolPickup : MonoBehaviour {

    public AudioClip pickupSound;
    public GameObject pistol; //Actual pistol under the FPS Controller

	public void PickupPistol()
    {
        GetComponent<AudioSource>().PlayOneShot(pickupSound);
        pistol.SetActive(true);

        Destroy(gameObject, pickupSound.length);
    }
}
