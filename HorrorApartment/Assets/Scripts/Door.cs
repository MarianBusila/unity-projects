using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorClosedAngle = 0f;

    public float smooth = 2f; //this is the speed of the rotation
    public AudioSource audioSource;

    public bool isLocked = false;
    public AudioClip isLockedAudioClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeDoorState()
    {
        if (isLocked != true)
        {
            open = !open;


            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            if(isLockedAudioClip != null)
                audioSource.PlayOneShot(isLockedAudioClip);
        }

    }
	
	
	// Update is called once per frame
	void Update () {
	    if(open)
        {
            //open the door
            Quaternion targetRotationOpen = Quaternion.Euler(0f, doorOpenAngle, 0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
        }
        else
        {
            //close the door
            Quaternion targetRotationClosed = Quaternion.Euler(0f, doorClosedAngle, 0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClosed, smooth * Time.deltaTime);
        }
	}
}
