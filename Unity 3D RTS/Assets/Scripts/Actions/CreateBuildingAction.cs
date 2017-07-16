using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuildingAction : ActionBehaviour
{
    public float Cost = 0;
    public GameObject BuildingPrefab;
    public float MaxBuildDistance = 30;

    public GameObject GhostBuildingPrefab;
    private GameObject active = null;

    public override Action GetClickAction()
    {
        return delegate()
        {
            var player = GetComponent<Player>().Info;
            if(player.Credits < Cost)
            {
                Debug.Log("Not enough credits");
                return;
            }
            var go = GameObject.Instantiate(GhostBuildingPrefab);
            var finder = go.AddComponent<FindBuildingSite>();
            finder.BuildingPrefab = BuildingPrefab;
            finder.MaxBuildDistance = MaxBuildDistance;
            finder.Info = player;
            finder.Source = transform;
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
