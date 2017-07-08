using UnityEngine;
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

        // weapons

        List<BaseStat> weaponStats = new List<BaseStat>();

        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Power, 6, "Power", "Your power level"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.AttackSpeed, 6, "Attack Speed", "Your agility in combat"));
        weaponStats.Add(new BaseStat(BaseStat.BaseStatType.Toughness, 6, "Toughness", "Your defensive toughness"));



        Item currentItem = new Item(weaponStats, "sword");
        currentItem.ItemType = Item.ItemTypes.Weapon;
        currentItem.ItemName = "sword";

        currentItem.Description = "a sharp blade, good for cutting";
        currentItem.ActionName = "Equip";

        //       currentItem.ObjectSlug = "sword";

        Items.Add(currentItem);


        currentItem = new Item(weaponStats, "staff");
        currentItem.ItemName = "staff";

        currentItem.Description = "a magic staff of conjuration";
        currentItem.ActionName = "Equip";

        //  currentItem.ObjectSlug = "staff";

        Items.Add(currentItem);

        //Items.Add(new Item(weaponStats, "sword"));
        //Items.Add(new Item(weaponStats, "staff"));


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
