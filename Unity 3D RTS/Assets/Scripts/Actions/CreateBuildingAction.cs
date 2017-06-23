using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuildingAction : ActionBehaviour
{
    public override Action GetClickAction()
    {
        return delegate()
        {
            Debug.Log("Create Command Base Attempt");
        };
    }
}
