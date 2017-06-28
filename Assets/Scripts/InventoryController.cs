using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

    public PlayerWeaponController playerWeaponController;
    public PlayerConsumableController playerConsumableController;

    public Item sword;

    public Item potion;

    private void Start()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();
        playerConsumableController = GetComponent<PlayerConsumableController>();

        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(6, "Power","Your Power Level"));

        // temp sword/staff slug swap
        sword = new Item(swordStats, "staff");

        potion = new Item(new List<BaseStat>(), "potion","drink this to test potipn drinking","Drink","PotionTest",false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {

       //     sword.ObjectSlug = "sword";

            playerWeaponController.EquipWeapon(sword);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            playerConsumableController.ConsumeItem(potion);
        }



        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //{

        //    sword.ObjectSlug = "sword";

        //    playerWeaponController.EquipWeapon(sword);
        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{

        //    sword.ObjectSlug = "staff";

        //    playerWeaponController.EquipWeapon(sword);
        //}




    }

}
