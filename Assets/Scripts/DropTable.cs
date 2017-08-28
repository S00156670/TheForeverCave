using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable
{
    public List<LootDrop> loot;

    public  Item GetDrop()
    {
        int roll = Random.Range(0,101);
        int dropCount = 0;

        foreach (LootDrop drop in loot)
        {
            dropCount += drop.DropChance;

             if (roll < dropCount)
            {
                return ItemDatabase.instance.GetItem(drop.ItemSlug);
            }
        }
        return null;
    }
}

public class LootDrop
{
    public string ItemSlug { get; set; }
    public int DropChance { get; set; }


    public LootDrop(string itmeSlug, int dropChance)
    {
        this.ItemSlug = itmeSlug;
        this.DropChance = dropChance;
    }


}