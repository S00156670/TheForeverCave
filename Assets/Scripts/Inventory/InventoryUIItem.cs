using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour {

    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        SetUpItemValues();
    }

    void SetUpItemValues()
    {
        this.transform.FindChild("Name").GetComponent<Text>().text = item.ItemName;
    }

    public void OnSelectItemButton()
    {

        Debug.Log("inventory item button clicked");
        InventoryController.instance.SetItemDetails(item, GetComponent<Button>());



    }
}
