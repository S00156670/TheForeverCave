using UnityEngine;
using System.Collections;
using System;

public class Potion : MonoBehaviour , IConsumable {
    public void Consume()
    {
        Debug.Log("You drank some potion"); 
    }

    public void Consume(CharachterStats stats)
    {
        Debug.Log("You drank some stat potion ");
    }

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
