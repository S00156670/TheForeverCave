using UnityEngine;
using System.Collections;
using System;

public class InventoryUI : MonoBehaviour {

    public RectTransform inventoryPannel; // 2d equivilent of a transform
    public RectTransform scrollViewContent;

    InventoryUIItem itemContainer { get; set; }

    bool menuIsActive { get; set; }

    Item currentSelection { get; set; }

    // Use this for initialization
    void Start ()
    {
        UIEventHandler.OnItemAddedToInventory += ItemAdded;

        itemContainer = Resources.Load<InventoryUIItem>("UI/Item_Container");

        inventoryPannel.gameObject.SetActive(false);
    }

    private void ItemAdded(Item item)
    {
        // add a slot for new item
        InventoryUIItem emptyItem = Instantiate(itemContainer);
        // set item to be the item recieved by method
        emptyItem.SetItem(item);
        // set into UI heirarchey
        emptyItem.transform.SetParent(scrollViewContent);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.I))
        {
                // toggle inventory
                menuIsActive = !menuIsActive;

                inventoryPannel.gameObject.SetActive(menuIsActive);
        }

	}
}
