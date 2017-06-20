using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseStat {

    public List<StatBonus> BaseAdditives { get; set; }

    public int BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; }

    public BaseStat(int baseValue, string statName, string statDescription)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.BaseValue = baseValue;
        this.StatName = statName;
        this.StatDescription = statDescription;

    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        //this.BaseAdditives.Remove(statBonus);
        // in final this will also need to check more detailed removing :: proper buff(relevent to item/potion etc) from correct stat
        this.BaseAdditives.Remove(BaseAdditives.Find(x => x.BonusValue == statBonus.BonusValue));
    }

    public int GetCalculatedStatValue()
    {
        this.FinalValue = 0;
        // check base value and add relevent buffs
        this.BaseAdditives.ForEach(x => this.FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        return FinalValue;
    }


}


public class StatBonus
{
    public int BonusValue { get; set; }

    public StatBonus(int bonusValue)
    {
        // this will need overloads for 2 buff types
        // timed buff(from consumables/status effects) & permanent buff (from equipped objects)
        this.BonusValue = bonusValue;
        Debug.Log("new stat bonus activated");
    }

}