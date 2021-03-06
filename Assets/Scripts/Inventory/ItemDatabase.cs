﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {


    public static ItemDatabase instance;

    //  public List<Item> Items { get; set; }
    private List<Item> Items = new List<Item>();

    // private List<Item> Items { get; set; }

    // Use this for initialization
    // awake happens faster than start
    void Awake () {

        Debug.Log(" item list initialization");

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            // there can only be only one
        }
        else
        {
            instance = this;
        }


        // should change the name of this to buid item list
        BuildDatabase();

        // then seperate method for if dungeon == true
        // buildMonsterList()
        // generate dungeon
  
    }


    private void BuildDatabase()
    {
        // options
        // generate pile of objects here
        // text file <- much benefit for time? 
        // json  <- maybe (saladLab)
        // sql  <- please no

        Debug.Log("BUILDING GAME ITEM LIST");

        // Weapons
        //sword
        List<BaseStat> weaponStats = new List<BaseStat>();
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.MeleeSkill, 6, "Melee", "Your melee level"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Agility, 4, "Atk Spd", "Your agility in combat"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Toughness, 5, "Toughness", "Your defensive toughness"));
        //weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Power, 6, "Power", "Your power level"));
        //weaponStats.Add(new BaseStat(BaseStat.BaseStatType.AttackSpeed, 6, "Atk Spd", "Your agility in combat"));
        //weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Toughness, 6, "Toughness", "Your defensive toughness"));

        Item currentItem = new Item(weaponStats, "sword");
        currentItem.ItemType = Item.ItemTypes.Weapon;
        currentItem.ItemName = "sword";
        currentItem.Description = "a sharp blade, good for cutting";
        currentItem.ActionName = "Equip";
        Items.Add(currentItem);

        ////knife
        weaponStats = new List<BaseStat>();
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.MeleeSkill, 5, "Melee", "Your melee level"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Agility, 5, "Atk Spd", "Your agility in combat"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Toughness, 3, "Toughness", "Your defensive toughness"));

        currentItem = new Item(weaponStats, "knife");
        currentItem.ItemName = "knife";
        currentItem.Description = "a small sharp blade";
        currentItem.ActionName = "Equip";
        Items.Add(currentItem);

        //staff
        weaponStats = new List<BaseStat>();
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.RangedSkill, 5, "Melee", "Your melee level"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Agility,     2, "Atk Spd", "Your agility in combat"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Toughness,   1, "Toughness", "Your defensive toughness"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.MagicSkill,  7, "Magic", "Your magic ability level"));

        currentItem = new Item(weaponStats, "staff");
        currentItem.ItemName = "staff";
        currentItem.Description = "a magic staff of conjuration";
        currentItem.ActionName = "Equip";
        Items.Add(currentItem);

        //ball
        weaponStats = new List<BaseStat>();
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.RangedSkill, 5, "Melee", "Your melee level"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Agility, 2, "Atk Spd", "Your agility in combat"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Vitality, 2, "Toughness", "Your defensive toughness"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Toughness, 1, "Toughness", "Your regeneration"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.MagicSkill, -3, "Magic", "Your magic ability level"));

        currentItem = new Item(weaponStats, "ball");
        currentItem.ItemName = "ball";
        currentItem.Description = "a leather ball made from the flayed skin of a fallen angel, it eminates a divine aura";
        currentItem.ActionName = "Equip";
        Items.Add(currentItem);


        //shuriken
        weaponStats = new List<BaseStat>();
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.RangedSkill, 5, "Melee", "Your melee level"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Agility, 2, "Atk Spd", "Your agility in combat"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Vitality, 2, "Vitality", "Your regeneration"));


        currentItem = new Item(weaponStats, "shuriken");
        currentItem.ItemName = "shuriken";
        currentItem.Description = "a single use thrown weapon with fearsome damage";
        currentItem.ActionName = "Equip";
        Items.Add(currentItem);

        Debug.Log(" weapons added");

        // consumables
        currentItem = (new Item(new List<BaseStat>(), "potion", "drink this to test potipn drinking", Item.ItemTypes.Consumable , "Drink", "PotionTest", false));
        currentItem.ItemType = Item.ItemTypes.Consumable;

        Items.Add(currentItem);


        // should work cleaner like this
        //    Items.Add(new Item(new List<BaseStat>(), "potion", "drink this to test potipn drinking", Item.ItemTypes.Consumable, "Drink", "PotionTest", false));

        // Items.Add(new Item(new List<BaseStat>(), "potion", "drink this to test potipn drinking", "Drink", "PotionTest", false));

        Debug.Log(" consumables added");

        Debug.Log(" item list complete");

        foreach (Item i in Items)
        {
            Debug.Log("DB List, item added : " + i.ObjectSlug + " - " + i.ItemType);

        }

    }


    public Item GetItem(string itemSlug)
    {
        foreach (Item i in Items)
        {
            if (i.ObjectSlug == itemSlug)
            {
                Debug.Log("found : " + i.ItemName);
                return i;
            }
        }

        Debug.Log("could not find item : " + itemSlug);
        return null;
    }

}
