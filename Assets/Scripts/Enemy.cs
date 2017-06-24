using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour , IEnemy {

    public float  power, toughness , maxHealth;
    public float currentHealth;


    public void PerformAttack()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
   
        currentHealth = currentHealth - amount;

        if (currentHealth <= 0)
            die();

    }

    // Use this for initialization
    void Start () {

        currentHealth = maxHealth;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void die()
    {
        // must also drop loot from this method
        Destroy(gameObject);
    }
}
