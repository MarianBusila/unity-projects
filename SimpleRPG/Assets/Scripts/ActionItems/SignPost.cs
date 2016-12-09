using UnityEngine;
using System.Collections;

public class SignPost : ActionItem {

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interacting with SignPost class");
    }
}
