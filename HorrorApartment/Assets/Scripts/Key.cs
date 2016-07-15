using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    public Door myDoor;
    AudioSource audioSource;

    public void UnlockDoor()
    {
        Debug.Log("UnlockDoor");
        myDoor.isLocked = false;
        audioSource = GetComponent<AudioSource>();        
        audioSource.Play();

        StartCoroutine("WaitForSelfDestruct");
        
    }

    IEnumerator WaitForSelfDestruct()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
