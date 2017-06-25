using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBuildingSite : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        var tempTarget = RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
        if(tempTarget.HasValue == false)
        {
            return;
        }

        transform.position = tempTarget.Value;
	}
}
