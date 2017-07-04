using UnityEngine;
using System.Collections;

public class UIEventHandler : MonoBehaviour {

    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInventory;

    public static void ItemAddadToInventory(Item item)
    {
        OnItemAddedToInventory(item);
    }


	//// Use this for initialization
	//void Start () {
	
	//}
	

}
