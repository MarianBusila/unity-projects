using UnityEngine;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour {

    public List<BaseStat> stats = new List<BaseStat>();

    void Start()
    {
        stats.Add(new BaseStat(4, "Power", "Your power level."));
        stats[0].AddStatBonus(new StatBonus(5));
        Debug.Log(stats[0].GetCalculatedStatValue());
    }
}
