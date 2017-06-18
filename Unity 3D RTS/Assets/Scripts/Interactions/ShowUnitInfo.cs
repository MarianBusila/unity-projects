using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnitInfo : Interaction {

    public string Name;
    public float MaxHealth, CurrentHealth;
    public Sprite ProfilePic;

    public override void Deselect()
    {
        InfoManager.Current.ClearPic();
        InfoManager.Current.ClearLines();
    }

    public override void Select()
    {
        InfoManager.Current.SetPic(ProfilePic);
        InfoManager.Current.SetLines(Name, CurrentHealth.ToString() + " / " + MaxHealth.ToString(), "Owner: " + GetComponent<Player>().Info.Name);
    }
}
