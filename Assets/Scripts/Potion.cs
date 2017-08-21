using UnityEngine;
using System.Collections;
using System;

public class Potion : MonoBehaviour , IConsumable {
    public void Consume()
    {
        Player player;
        player = GameObject.Find("Player").GetComponent<Player>();
        player.currentHealth += 10;

        UIEventHandler.PlayerHealthChanged(player.currentHealth, player.maxHealth);

        Debug.Log("You drank some potion"); 
    }

    public void Consume(CharachterStats stats)
    {
        Player player;
        player = GameObject.Find("Player").GetComponent<Player>();
        player.currentHealth += 10;

        UIEventHandler.PlayerHealthChanged(player.currentHealth, player.maxHealth);

        Debug.Log("You drank some stat potion ");
    }

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
