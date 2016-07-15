using UnityEngine;
using System.Collections;

public class ScareTrigger : MonoBehaviour {

    public AudioSource scareAudioSource;
    public Light scareLight;
    bool hasPlayedAudio;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayedAudio)
        {
            scareAudioSource.Play();
            hasPlayedAudio = true;
            scareLight.enabled = true;
        }
    }
}
