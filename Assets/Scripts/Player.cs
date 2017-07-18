﻿using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {


    public CharachterStats charachterStats;

    public int currentHealth;
    public int maxHealth;


    // Use this for initialization
    void Awake () {

        charachterStats = new CharachterStats(5,5,5);

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
