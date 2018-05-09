using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlockAcademy : Academy {

    // the walking speed of agent in the scene
    public float agentRunSpeed;

    public float agentRotationSpeed;

    // .9 means 90% of spawn area will be used
    public float spawnAreaMarginMultiplier;

    // when a goal is scored the ground will change
    public Material goalScoredMaterial;

    // when agent fails, the ground will change
    public Material failMaterial;

    public float gravityMultiplier = 1.0f;

    void State()
    {
        Physics.gravity *= gravityMultiplier;
    }
}
