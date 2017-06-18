using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashBoxManager : MonoBehaviour {

    public Text CashField;
	
	// Update is called once per frame
	void Update () {
        CashField.text = "$ " + (int)Player.Default.Credits;
		
	}
}
