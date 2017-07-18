using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharachterStats  {

    public List<BaseStat> stats = new List<BaseStat>();

    public CharachterStats(int power, int toughness, int attachSpeed)
    {

        //  stats.Add();
        stats = new List<BaseStat>();// { new BaseStat(); };

        // changed Attack Speed to Atk Speed so it fits better in ui, remember this as it may cause issues elsewhere if stat is refrenced by old name
        stats = new List<BaseStat>() {  new BaseStat( BaseStat.BaseStatType.Power, 4, "Power", "Your Power Level"),
                                        new BaseStat( BaseStat.BaseStatType.Toughness, 10, "Toughness", "Your Toughness Level"),
                                        new BaseStat( BaseStat.BaseStatType.AttackSpeed, 2, "Atk Speed", "Your Speed Level")


        };
    }

    private void Start()
    {
        // set up stats here
        // should be done mainly fron children (player , skelaton etc.)
        //stats.Add(new global::BaseStat(4, "Power", "Your Power Level"));
        //stats.Add(new global::BaseStat(10, "Vitality", "Your Vitality Level"));

        //stats.Add(new global::BaseStat(BaseStat.BaseStatType.Power, 4, "Power", "Your Power Level"));
        //stats.Add(new global::BaseStat(BaseStat.BaseStatType.Toughness, 10, "Toughness", "Your Toughness Level"));
        //stats.Add(new global::BaseStat(BaseStat.BaseStatType.AttackSpeed, 2, "Attack Speed", "Your Speed Level"));



        stats[0].AddStatBonus(new StatBonus(5));

        Debug.Log(stats[0].GetCalculatedStatValue());

    }

    public BaseStat GetStat(BaseStat.BaseStatType stat)
    {
        return this.stats.Find(x => x.StatType == stat);

    }



    public void AddStatBonus(List<BaseStat> statusChange)
    {
        foreach (BaseStat statCheck in statusChange)
        {
            // check for matching catagory then when found puts a bonus to the current stat being checked
            //stats.Find(x => x.StatName == statCheck.StatName).AddStatBonus(new StatBonus(statCheck.BaseValue));

            GetStat(statCheck.StatType).AddStatBonus(new StatBonus(statCheck.BaseValue));
        }


    }

    public void RemoveStatBonus(List<BaseStat> statusChange)
    {
        foreach (BaseStat statCheck in statusChange)
        {
            // check for matching catagory then when found takes a bonus from the current stat being checked
            GetStat(statCheck.StatType).RemoveStatBonus(new StatBonus(statCheck.BaseValue));
     //       stats.Find(x => x.StatName == statCheck.StatName).RemoveStatBonus(new StatBonus(statCheck.BaseValue));
        }
    }


}
