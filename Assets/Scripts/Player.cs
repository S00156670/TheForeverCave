using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {


    public CharachterStats charachterStats;

    public float currentHealth;
    public float maxHealth;


    // Use this for initialization
    void Start () {

        charachterStats = new CharachterStats(5,5,5);

        this.currentHealth = this.maxHealth;

	}

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Debug.Log(amount + " damage recieved by player");
        Debug.Log(currentHealth + " is current player health");

        if (currentHealth <= 0)
            Die();


    }

    private void Die()
    {
        Debug.Log("Player is dead , resetting health");
        this.currentHealth = maxHealth;

        // reset gameplay position from here

    }

    // Update is called once per frame
    void Update () {
	
	}
}
