using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour {

    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        SetUpItemVAlues();
    }

    void SetUpItemVAlues()
    {
        this.transform.FindChild("item_Name").GetComponent<Text>().text = item.ItemName;
    }

    public void OnSelectItemButton(Item item)
    {
        
    }
}
