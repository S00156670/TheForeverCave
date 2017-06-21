using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

    public Item sword;

    private void Start()
    {
        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(6, "Power","Your Power Level"));
        sword = new Item(swordStats,"sword");
    }


}
