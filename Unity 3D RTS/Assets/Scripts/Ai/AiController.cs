using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {

    public float Confusion = 0.3f;
    public float Frequency = 1;

    private float waited = 0;
    private List<AiBehavior> Ais = new List<AiBehavior>();
	// Use this for initialization
	void Start () {
		foreach(var ai in GetComponents<AiBehavior>())
            Ais.Add(ai);
	}
	
	// Update is called once per frame
	void Update () {
        waited += Time.deltaTime;
        if (waited < Frequency)
            return;

        float bestAiValue = float.MinValue;
        AiBehavior bestAi = null;

        foreach(var ai in Ais)
        {
            ai.TimePassed += waited;
            var aiValue = ai.GetWeight() * ai.WeightMultiplier + Random.Range(0, Confusion);
            if(aiValue > bestAiValue)
            {
                bestAi = ai;
                bestAiValue = aiValue;
            }
        }

        bestAi.Execute();
        waited = 0;
	}
}
