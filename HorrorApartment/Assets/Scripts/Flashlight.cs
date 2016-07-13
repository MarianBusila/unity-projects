using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

    Light flashLight;
    private bool isActive;
    AudioSource audioSource;

    public AudioClip soundFlashlightOn;
    public AudioClip soundFlashlightOff;

    // Use this for initialization
    void Start () {
        isActive = true;
        audioSource = GetComponent<AudioSource>();
        flashLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.F))
        {
            isActive = !isActive;
            flashLight.enabled = isActive;
            if (isActive)
                audioSource.PlayOneShot(soundFlashlightOn);
            else
                audioSource.PlayOneShot(soundFlashlightOff);

        }
	}
}
