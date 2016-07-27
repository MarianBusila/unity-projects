using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Note : MonoBehaviour {

    public Image noteImage;
    public AudioClip pickupSound;
    public AudioClip putAwaySound;

    public GameObject playerObject;

    // Use this for initialization
    void Start () {
        //noteImage.enabled = false;
        noteImage.gameObject.SetActive(false);
	}

    public void ShowNoteImage()
    {
        //noteImage.enabled = true;
        noteImage.gameObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(pickupSound);

        //disable the player controller
        playerObject.GetComponent<FirstPersonController>().enabled = false;
        //unlocks the mouse Coursor and makes it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideNoteImage()
    {
        //noteImage.enabled = false;
        noteImage.gameObject.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(putAwaySound);

        playerObject.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }
	
}
