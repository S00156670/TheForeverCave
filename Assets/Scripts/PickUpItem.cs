using UnityEngine;
using System.Collections;

public class PickUpItem : Interactable
{

    public override void Interact()
    {
        Debug.Log("interacting with pick-up Item");
    }

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}


public class Consumable : PickUpItem
{

    public override void Interact()
    {
        Debug.Log("interacting with Consumable");
    }

}

public class Weapon : PickUpItem
{

    public override void Interact()
    {
        Debug.Log("interacting with Weapon");
    }

}
public class Apperal : PickUpItem
{

    public override void Interact()
    {
        Debug.Log("interacting with Apperal");
    }

}
