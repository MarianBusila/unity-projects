﻿using UnityEngine;
using System.Collections;

public class WorldInteraction : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent playerAgent;

    void Start()
    {
        playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }
    }
	void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if(Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if(interactedObject.tag == "Interactable Object")
            {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else //move player
            {
                playerAgent.stoppingDistance = 0f;
                playerAgent.destination = interactionInfo.point;
            }
        }
    }
}
