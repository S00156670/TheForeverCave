using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {


    public CharachterStats charachterStats;

    public int currentHealth;
    public int maxHealth;

    public LevelExperienceHelper PlayerLevel { get; set; }

    // Use this for initialization
    void Awake () {

        PlayerLevel = GetComponent<LevelExperienceHelper>();

        // not setting in the window at first
        this.currentHealth = this.maxHealth;


        UIEventHandler.PlayerHealthChanged(this.currentHealth, this.maxHealth);

        Debug.Log("Player is created and awakened");

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
