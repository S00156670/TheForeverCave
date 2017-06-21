using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharachterStats : MonoBehaviour {

    public List<BaseStat> stats = new List<BaseStat>();

    private void Start()
    {
        // set up stats here
        // should be done mainly fron children (player , skelaton etc.)
        stats.Add(new global::BaseStat(4, "Power", "Your Power Level"));
        stats.Add(new global::BaseStat(10, "Vitality", "Your Vitality Level"));

        stats[0].AddStatBonus(new StatBonus(5));

        Debug.Log(stats[0].GetCalculatedStatValue());

    }

    public void AddStatBonus(List<BaseStat> statusChange)
    {
        foreach (BaseStat statCheck in statusChange)
        {
            // check for matching catagory then when found puts a bonus to the current stat being checked
            stats.Find(x => x.StatName == statCheck.StatName).AddStatBonus(new StatBonus(statCheck.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statusChange)
    {
        foreach (BaseStat statCheck in statusChange)
        {
            // check for matching catagory then when found takes a bonus from the current stat being checked
            stats.Find(x => x.StatName == statCheck.StatName).RemoveStatBonus(new StatBonus(statCheck.BaseValue));
        }
    }


}
