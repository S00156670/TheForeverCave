using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpItem : Interactable
{
    public Item ItemToPick { get; set; }

 //   public string pickUpSlug;

 //   public List<BaseStat> Stats { get; set; }

 //   public Transform Position { get; set; }

    public override void Interact()
    {
        Debug.Log("interacting with pick-up Item");
        InventoryController.instance.GiveItem(ItemToPick);
        Destroy(gameObject);
    }

    // public void FadeAway()
    //{
    //// maybe for some kinds of loot
    //// or for dead bodies if you harvest bodies the body could be a kid of pick up itelf
    //// would need a timer to work
    //}

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {
    ////check ofr collision here? maybe bob upa and down 
    //}
}


//public class Consumable : PickUpItem
//{

//    public override void Interact()
//    {
//        Debug.Log("interacting with Consumable");
//    }

//    private StatBonus ConsumableBuff;
//}

//public class Weapon : PickUpItem
//{

//    public override void Interact()
//    {
//        Debug.Log("interacting with Weapon");
//    }

//}
//public class Apperal : PickUpItem
//{

//    public override void Interact()
//    {
//        Debug.Log("interacting with Apperal");
//    }

//}
