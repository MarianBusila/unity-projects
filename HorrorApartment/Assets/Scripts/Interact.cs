using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{

    public string interactButton;
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    public Image interactIcon; //picture of the hand to show you can interact or not

    public bool isInteracting;

	// Use this for initialization
	void Start ()
    {
        if(interactIcon != null)
            interactIcon.enabled = false;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if(!isInteracting)
            {
                if (interactIcon != null)
                    interactIcon.enabled = true;

                if(Input.GetButtonDown(interactButton))
                {
                    if(hit.collider.CompareTag("Door"))
                    {                        
                        hit.collider.GetComponent<Door>().ChangeDoorState();
                    }
                    else if(hit.collider.CompareTag("Key"))
                    {                        
                        hit.collider.GetComponent<Key>().UnlockDoor();
                    }
                    else if (hit.collider.CompareTag("Safe"))
                    {   
                        hit.collider.GetComponent<Safe>().ShowSafeCanvas();                        
                    }
                    else if (hit.collider.CompareTag("Note"))
                    {
                        hit.collider.GetComponent<Note>().ShowNoteImage();

                    }
                    else if (hit.collider.CompareTag("Pistol"))
                    {
                        hit.collider.GetComponent<PistolPickup>().PickupPistol();
                    }
                }
            }
        }
        else
        {
            if (interactIcon != null)
                interactIcon.enabled = false;
        }
	}
}
