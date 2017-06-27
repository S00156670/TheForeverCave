using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

    public PlayerWeaponController playerWeaponController;
    public Item sword;

    private void Start()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();

        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(6, "Power","Your Power Level"));

        // temp sword/staff slug swap
        sword = new Item(swordStats,"staff");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerWeaponController.EquipWeapon(sword);
        }


        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    playerWeaponController.PerformAttack();
        //}

    }

}
