using UnityEngine;
using System.Collections;
using System;


//using UnityEngine.AI;


//public class Enemy : MonoBehaviour , IEnemy {

    public class Enemy : Interactable, IEnemy
    {

    public LayerMask aggroLayerMask;
    public float  power, toughness , maxHealth;
    public float currentHealth;

    private NavMeshAgent navAgent;
    private CharachterStats charachterStats;

    Collider[] aggroNavTargets;

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

        navAgent = GetComponent<NavMeshAgent>();
        charachterStats = new CharachterStats(9,10,2);

        currentHealth = maxHealth;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // chech player range
        //   Physics.OverlapSphere();

        aggroNavTargets = Physics.OverlapSphere(transform.position, 10 /*line of sight*/, aggroLayerMask);

        if (aggroNavTargets.Length > 0)
        {
            Debug.Log("enemy has spotted a player");

        }


    }

    public void die()
    {
        // must also drop loot from this method
        Destroy(gameObject);
    }
}
