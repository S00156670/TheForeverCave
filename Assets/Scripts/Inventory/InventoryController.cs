using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

    public PlayerWeaponController playerWeaponController;
    public PlayerConsumableController playerConsumableController;

    public InventoryUIDetails inventoryDetailsPannel;

    public List<Item> playerItems = new List<Item>();

    public static InventoryController instance { get; set; }

    //public Item sword;

    //public Item potion;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            // there can only be only one
        }
        else
        {
            instance = this;
        }

        playerWeaponController = GetComponent<PlayerWeaponController>();
        playerConsumableController = GetComponent<PlayerConsumableController>();

        Debug.Log("trying to add items to inventory");

        // adding test items
        GiveItem("knife");
        Debug.Log("got knife");

        //      GiveItem("potion");
        GiveItem("potion");
        GiveItem("potion");
        GiveItem("potion");
        Debug.Log("got potions");

        //GiveItem("sword");
        //Debug.Log("got sword");

        //GiveItem("staff");
        //Debug.Log("got staff");

        //GiveItem("shuriken");
        //GiveItem("shuriken");
        //GiveItem("shuriken");
        //GiveItem("shuriken");
        //Debug.Log("got shuriken");

        //GiveItem("ball");
        //Debug.Log("got ball");

        //// generating test hardcoded objects
        //List<BaseStat> swordStats = new List<BaseStat>();
        //swordStats.Add(new BaseStat(6, "Power","Your Power Level"));

        //// temp sword/staff slug swap
        //sword = new Item(swordStats, "staff");

        //potion = new Item(new List<BaseStat>(), "potion","drink this to test potipn drinking","Drink","PotionTest",false);
    }

    public void GiveItem(string itemSlug)
    {
        Item item = ItemDatabase.instance.GetItem(itemSlug);

        //playerItems.Add(ItemDatabase.instance.GetItem(itemSlug));
        playerItems.Add(item);

        Debug.Log(itemSlug + " added to player inventory");
        Debug.Log(playerItems.Count + " items in inventory");

       UIEventHandler.ItemAddedToInventory(item);
    }

    public void GiveItem(Item item)
    {
        playerItems.Add(item);

        Debug.Log(item.ObjectSlug + " added to player inventory");
        Debug.Log(playerItems.Count + " items in inventory");

        UIEventHandler.ItemAddedToInventory(item);
    }

    public void SetItemDetails(Item item , Button selectedButton)
    {
        inventoryDetailsPannel.SetItem(item, selectedButton);
    }



    public void EquipItem(Item itemToEquip)
    {
        // return currently equipped to inventory
        if (playerWeaponController.EquippedWeapon != null)
        {
            GiveItem(playerWeaponController.currentWeaponSlug);
        }

        playerWeaponController.EquipWeapon(itemToEquip);
    }


    public void ConsumeItem(Item itemToConsume)
    {
        playerConsumableController.ConsumeItem(itemToConsume, (GameObject.Find("Player").transform.position + new Vector3(0,2,0)));
    }

    ////test code for hardcoded items
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //   //     sword.ObjectSlug = "sword";
    //        playerWeaponController.EquipWeapon(sword);
    //    }
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        playerConsumableController.ConsumeItem(potion);
    //    }

    //    //if (Input.GetKeyDown(KeyCode.Keypad1))
    //    //{
    //    //    sword.ObjectSlug = "sword";
    //    //    playerWeaponController.EquipWeapon(sword);
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.Z))
    //    //{
    //    //    sword.ObjectSlug = "staff";

    //    //    playerWeaponController.EquipWeapon(sword);
    //    //}
    //}

}
