using UnityEngine;
using System.Collections;

public class Signpost : Interactable
{
    public string[] dialogue;

    public override void Interact()
    {
        DialogueManager.Instance.AddNewDialogue(dialogue,"Signpost");
        Debug.Log("interacting with Signpost");
    }
    
}
