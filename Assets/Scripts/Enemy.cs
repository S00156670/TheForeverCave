﻿using UnityEngine;
using System.Collections;
using System;


//using UnityEngine.AI;


//public class Enemy : MonoBehaviour , IEnemy {

    public class Enemy : Interactable, IEnemy
    {

    public LayerMask aggroLayerMask;
    public float  power, toughness , maxHealth;
    public float currentHealth;

    private UnityEngine.AI.NavMeshAgent navAgent;
    private CharachterStats charachterStats;

    Collider[] aggroNavTargets;

    Player player;

    public int Experience{get;set;}




    public void PerformAttack()
    {
        player.TakeDamage(5);
        Debug.Log(this.name + "is attacking player");
    }

    public void TakeDamage(int amount)
    {
   
        currentHealth = currentHealth - amount;

        Debug.Log(this.name + " revieved " +  amount + " damage");

        if (currentHealth <= 0)
            Die();

    }

    // Use this for initialization
    void Start () {
        Experience = 20;
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        charachterStats = new CharachterStats(9,10,2);

        currentHealth = maxHealth;
	
	}

    // FixedUpdate is called less often than update but still often enough for smooth play
    void FixedUpdate () {

        // chech player range
        //   Physics.OverlapSphere();

        aggroNavTargets = Physics.OverlapSphere(transform.position,
                            /*line of sight*/10 ,
                            aggroLayerMask);

        if (aggroNavTargets.Length > 0)
        {
            //          Debug.Log("enemy has spotted a player");
            ChasePlayer(aggroNavTargets[0].GetComponent<Player>());
        }


    }

     void ChasePlayer( Player targetPlayer)
    {
        this.player = targetPlayer;
        navAgent.SetDestination(player.transform.position);

        // check if within attack range
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            // cause method to be triggered around a specified amount of seconds
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", .5f, 2f);

            }
        }
        else
        {
            CancelInvoke("PerformAttack");
        }

        navAgent.SetDestination(player.transform.position);
    }

    public void Die()
    {
        // must also drop loot from this method
        Destroy(gameObject);
    }
}
