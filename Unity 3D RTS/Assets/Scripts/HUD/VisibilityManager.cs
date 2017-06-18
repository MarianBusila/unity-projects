using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityManager : MonoBehaviour {

    public float timeBetweenChecks = 1;
    public float VisibleRange = 100;

    private float waited = 10000;
	
	// Update is called once per frame
	void Update ()
    {
        waited += Time.deltaTime;
        if(waited <= timeBetweenChecks)
        {
            return;
        }

        //reset 
        waited = 0;
        List<MapBlip> playerBlips = new List<MapBlip>();
        List<MapBlip> oponentBlips = new List<MapBlip>();

        foreach(var p in RtsManager.Current.Players)
        {
            foreach(var u in p.ActiveUnits)
            {
                var blip = u.GetComponent<MapBlip>();
                if(p == Player.Default)
                {
                    playerBlips.Add(blip);
                }
                else
                {
                    oponentBlips.Add(blip);
                }
            }
        }

        foreach(var oponentBlip in oponentBlips)
        {
            bool active = false;
            foreach(var playerBlip in playerBlips)
            {
                var distance = Vector3.Distance(oponentBlip.transform.position, playerBlip.transform.position);
                if (distance <= VisibleRange)
                {
                    active = true;
                    break;
                }
            }

            oponentBlip.Blip.SetActive(active);
            
            foreach (var r in oponentBlip.GetComponentsInChildren<Renderer>())
            {
                r.enabled = active;
            }
            
        }
        
    }
}
