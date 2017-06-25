using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuildingAction : ActionBehaviour
{
    public GameObject GhostBuildingPrefab;
    private GameObject active = null;

    public override Action GetClickAction()
    {
        return delegate()
        {
            var go = GameObject.Instantiate(GhostBuildingPrefab);
            go.AddComponent<FindBuildingSite>();
            active = go;
        };
    }

    private void Update()
    {
        if (active == null)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Destroy(active);
        }
    }

    private void OnDestroy()
    {
        if(active == null)
        {
            return;
        }

        Destroy(active);
    }
}
