using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item  {

    public List<BaseStat> Stats { get; set; }
    // a slug is a term for a simplified object definition , (probaly in the end will also define a mesh)
    public string ObjectSlug { get; set; }

    public string Description;
    public string ActionName; // maybe enum for final version eat,drink,throw etc
    public string ItemName;

    public bool StatModifier;


    //  public bool InstantTrigger;
    //  for trap items?

    public Item(List<BaseStat> stats , string objectSlug)
    {
        Stats = stats;
        ObjectSlug = objectSlug;

    }

    public Item(List<BaseStat> stats, string objectSlug, string description, string actionName, string itemName, bool statModifier)
    {
        Stats = stats;
        ObjectSlug = objectSlug;
        Description = description;
        ActionName = actionName;
        ItemName = itemName;
        StatModifier = statModifier;



    }

}
