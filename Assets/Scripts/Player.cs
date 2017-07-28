using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {


    public CharachterStats charachterStats;

    public int currentHealth;
    public int maxHealth;


    // Use this for initialization
    void Awake () {


        Debug.Log("Player is created and awakened");

        charachterStats = new CharachterStats(5,5,5);

        // not setting in the window at first
        this.currentHealth = this.maxHealth;

        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);
	}

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Debug.Log(amount + " damage recieved by player");
        Debug.Log(currentHealth + " is current player health");

        if (currentHealth <= 0)
            Die();

        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

    }

    private void Die()
    {
        Debug.Log("Player is dead , resetting health");
        this.currentHealth = maxHealth;

        // reset gameplay position from here
        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
