using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryUIDetails : MonoBehaviour {
    Item item;

    Button selectedItemButton;
    Button itemInteractButton;

    Text itemNameText;
    Text itemDescriptionText;
    Text itemInteractButtonText;

    public Text itemStatText;

    private void Start()
    {
        
        itemNameText = transform.FindChild("Item_Name").GetComponent<Text>();
        itemDescriptionText = transform.FindChild("Item_Description").GetComponent<Text>();


        itemInteractButton = transform.FindChild("Button").GetComponent<Button>();
        itemInteractButtonText = itemInteractButton.transform.FindChild("Text").GetComponent<Text>();

        gameObject.SetActive(false);
    }


    public void SetItem(Item item, Button selectedButton)
    {
        // activate details pannel
        gameObject.SetActive(true);

        itemStatText.text = "";

        if (item.Stats != null)
        {

            foreach (BaseStat stat in item.Stats)
            {
                itemStatText.text += stat.StatName + " : " +  stat.BaseValue;
            }

        }





        // must clear listeners
        // otherwise you wind up with a stack of listeners and things happen multiple times
        itemInteractButton.onClick.RemoveAllListeners();

        this.item = item;
        selectedItemButton = selectedButton;

        // to update details pannel
        itemNameText.text = item.ItemName;
        itemDescriptionText.text = item.Description;
        itemInteractButtonText.text = item.ActionName;

        itemInteractButton.onClick.AddListener(OnItemInteract);
    }


    public void OnItemInteract()
    {
        // would a switch statement be more efficent
        // here we use the item
        if (item.ItemType == Item.ItemTypes.Consumable)
        {
        InventoryController.instance.ConsumeItem(item);
            Destroy(selectedItemButton.gameObject);
        }
        else if (item.ItemType == Item.ItemTypes.Weapon)
        {
            InventoryController.instance.EquipItem(item);
            Destroy(selectedItemButton.gameObject);
        }

        // clear data after use
        item = null;
        // deactivate deails pannel (looks better this way)
        gameObject.SetActive(false);

    }

}
