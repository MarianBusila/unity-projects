using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour {

    private Renderer _renderer;
	// Use this for initialization
	void Start () {
        _renderer = gameObject.GetComponent<Renderer>();
	}

    public void OnEnter()
    {
        _renderer.material.color = Color.red;
    }

    public void OnExit()
    {
        _renderer.material.color = Color.white;
    }
}
