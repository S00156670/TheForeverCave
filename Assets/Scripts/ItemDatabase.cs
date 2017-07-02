using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {


    public static ItemDatabase instance;

    //  public List<Item> Items { get; set; }
    private List<Item> Items = new List<Item>();

    // private List<Item> Items { get; set; }

    // Use this for initialization
    void Start () {

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

        BuildDatabase();

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
        weaponStats.Add(new BaseStat(6, "Power", "Your Power Level"));

     //   Item weapon = new Item(weaponStats, "sword");



             Items.Add(new Item(weaponStats, "sword"));
             Items.Add(new Item(weaponStats, "staff"));


        Debug.Log(" weapons added");

        // consumables
        Items.Add(new Item(new List<BaseStat>(), "potion", "drink this to test potipn drinking", "Drink", "PotionTest", false));

        Debug.Log(" consumables added");

        Debug.Log(" item list complete");

        foreach (Item i in Items)
        {
            Debug.Log(" item added : " + i.ObjectSlug );

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

        Debug.Log("could not find item");
        return null;

    }

}
