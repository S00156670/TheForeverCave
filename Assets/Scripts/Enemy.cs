using UnityEngine;
using System.Collections;
using System;

//public class Enemy : MonoBehaviour , IEnemy {

    public class Enemy : Interactable, IEnemy
    {

        public float  power, toughness , maxHealth;
    public float currentHealth;

    private CharachterStats charachterStats;


    public void PerformAttack()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
   
        currentHealth = currentHealth - amount;

        Debug.Log(this.name + " revieved " +  amount + " damage");

        if (currentHealth <= 0)
            die();

    }

    // Use this for initialization
    void Start () {

        charachterStats = new CharachterStats(9,10,2);

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
