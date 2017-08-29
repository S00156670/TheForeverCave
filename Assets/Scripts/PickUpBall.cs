using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpBall : ActionItem
{

    public List<Item> chestItems;
    private InventoryController inventory;

    private void Start()
    {
        chestItems = new List<Item>();
        inventory = GameObject.Find("Player").GetComponent<InventoryController>();

        ItemDatabase db = GameObject.Find("Inventory").GetComponent<ItemDatabase>();
        chestItems.Add(db.GetItem("ball"));
    }


    public override void Interact()
    {
        Debug.Log("Interacting with Treasure Chest");
        foreach (Item item in chestItems)
        {
            // add item to inventory
            Debug.Log("PLAYER FOUND THE BALL " + item.ItemName);
            inventory.GiveItem(item);
        }
        Destroy(gameObject);
    }

    public void StoreItem(Item item)
    {
        chestItems.Add(item);
    }
}
