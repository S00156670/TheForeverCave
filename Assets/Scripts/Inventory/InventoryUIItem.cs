using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour {

    public Item item;

    public Text itemText;
    public Image itemImage;

    // is this receiving an item
    public void SetItem(Item item)
    {

    //    itemText.text = "";
    //    itemImage = new Image();

        if (item == null)
        {
            Debug.Log("UI WARNING - attempting to set null item");
        }
        else {
            Debug.Log(" UI SetItem" + item.ItemName + " " + item.ObjectSlug + " " );
        }

        this.item = item;
        SetUpItemValues();
        
    }

    void SetUpItemValues()
    {
          this.transform.Find("Name").GetComponent<Text>().text = item.ItemName;
        //    this.transform.FindChild("Item_Icon").GetComponent<Sprite>().texture = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug).texture;
        //itemText.text = item.ItemName;
        //itemImage.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);

  //      itemText.text = item.ItemName;
  //      itemImage.sprite = Resources.Load<Sprite>("UI/Icons/Items/" + item.ObjectSlug);

    }

    public void OnSelectItemButton()
    {

        Debug.Log("inventory item button clicked");
        InventoryController.instance.SetItemDetails(item, GetComponent<Button>());



    }
}
