using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharachterStats  {

    public List<BaseStat> stats = new List<BaseStat>();

    public CharachterStats(int power, int toughness, int attachSpeed)
    {

        //  stats.Add();
        stats = new List<BaseStat>();// { new BaseStat(); };

        // maybe change Attack Speed to Atk Spd so it fits better in ui, remember this as it may cause issues elsewhere if stat is refrenced by old name
        stats = new List<BaseStat>() {  new BaseStat( BaseStat.BaseStatType.Power, power, "Power", "Your Power Level"),
                                        new BaseStat( BaseStat.BaseStatType.Toughness, toughness, "Toughness", "Your Toughness Level"),
                                        new BaseStat( BaseStat.BaseStatType.AttackSpeed, attachSpeed, "Atk Spd", "Your Speed Level")
        };
    }

    public CharachterStats(int health,int vitality , int toughness,int meleeSkill,int rangedSkill, int agility, int magicSkill)
    {
       stats = new List<BaseStat>();

       stats = new List<BaseStat>() {  new BaseStat( BaseStat.BaseStatType.Health, health, "Health", "Your Health Level"),
                                        new BaseStat( BaseStat.BaseStatType.Vitality, vitality, "Vitality", "Your Recovery Rate"),
                                        new BaseStat( BaseStat.BaseStatType.Toughness, toughness, "Toughness", "Your Defensive Ability"),
                                        new BaseStat( BaseStat.BaseStatType.MeleeSkill, meleeSkill, "Melee", "Close Combat Skill"),
                                        new BaseStat( BaseStat.BaseStatType.RangedSkill, rangedSkill, "Ranged", "Ranged Combat skill"),
                                        new BaseStat( BaseStat.BaseStatType.Agility, agility, "Agility", "Your Speed Level"),
                                        new BaseStat( BaseStat.BaseStatType.MagicSkill, magicSkill, "Magic", "Your Magic Level")
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
