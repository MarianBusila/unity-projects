using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiBehavior : MonoBehaviour {

    public abstract float GetWeight();

    public abstract float Execute(); 
}
