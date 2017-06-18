using UnityEngine;
using System.Collections;

public class NPC : Interactable {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    public string[] dialogue;
    public string name;

    public override void Interact()
    {
        Debug.Log("interacting with NPC");
        DialogueManager.Instance.AddNewDialogue(dialogue,name);
    }


}
