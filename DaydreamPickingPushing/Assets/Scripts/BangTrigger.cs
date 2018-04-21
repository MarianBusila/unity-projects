using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangTrigger : MonoBehaviour {

    private AudioSource bangAudioSource;

    private void Start()
    {
        bangAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bangAudioSource.Play();
        Debug.Log("Bang");
    }
}
