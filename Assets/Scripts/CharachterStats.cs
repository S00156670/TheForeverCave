using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharachterStats : MonoBehaviour {

    public List<BaseStat> stats = new List<BaseStat>();

    private void Start()
    {
        stats.Add(new global::BaseStat(4, "Power", "Your Power Level"));
        stats[0].AddStatBonus(new StatBonus(5));

        Debug.Log(stats[0].GetCalculatedStatValue());

    }

}
