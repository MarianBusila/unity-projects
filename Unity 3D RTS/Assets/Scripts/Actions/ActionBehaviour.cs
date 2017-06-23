using System;
using UnityEngine;

public abstract class ActionBehaviour : MonoBehaviour {

    public abstract Action GetClickAction();

    public Sprite ButtonPic;
    
}
