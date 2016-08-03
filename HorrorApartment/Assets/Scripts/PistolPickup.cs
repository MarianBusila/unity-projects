using UnityEngine;
using System.Collections;

public class PistolPickup : MonoBehaviour {

    public AudioClip pickupSound;
    public GameObject pistol; //Actual pistol under the FPS Controller

    public GameObject ghoul; //reference to the actual model
    public GameObject ghoulWorldModel; //reference to the world model

	public void PickupPistol()
    {
        GetComponent<AudioSource>().PlayOneShot(pickupSound);
        pistol.SetActive(true);

        ghoul.SetActive(true);
        ghoulWorldModel.SetActive(false);

        Destroy(gameObject, pickupSound.length);
    }
}
