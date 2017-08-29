using UnityEngine;
using System.Collections;
using System;


//using UnityEngine.AI;


//public class Enemy : MonoBehaviour , IEnemy {

    public class Enemy : Interactable, IEnemy
    {
    public LayerMask aggroLayerMask;
  //  public float power;
 //   public float toughness;
    private float maxHealth;
    private float currentHealth;

    private UnityEngine.AI.NavMeshAgent navAgent;
    public CharachterStats charachterStats;

    Collider[] aggroNavTargets;

    Player player;

    public int Experience{get;set;}

    public DropTable dropTable { get; set; }

    public PickUpItem pickUpItem;

    // Use this for initialization
    void Start()
    {
        dropTable = new DropTable();
        dropTable.loot = new System.Collections.Generic.List<LootDrop>
                        {
                            new LootDrop("sword",25),
                            new LootDrop("staff",25),
                            new LootDrop("potion",25),
                            new LootDrop("ball",25),
                        };

        Experience = 20;
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //     charachterStats = new CharachterStats(9, 10, 2);
        charachterStats = new CharachterStats(3, 1, 2, 5, 7, 4, 2);

        maxHealth = charachterStats.GetStat(BaseStat.BaseStatType.Health).GetCalculatedStatValue() * 10;

        currentHealth = maxHealth;

    }


    public void PerformAttack()
    {
        //  player.TakeDamage(5);
        player.TakeDamage(charachterStats.GetStat(BaseStat.BaseStatType.MeleeSkill).GetCalculatedStatValue());
        Debug.Log(this.name + "is attacking player");
    }

    public void TakeDamage(int amount)
    {
        amount -= charachterStats.GetStat(BaseStat.BaseStatType.Toughness).GetCalculatedStatValue();

        if (amount > 0)
        currentHealth = currentHealth - amount;

        Debug.Log(this.name + " revieved " +  amount + " damage");

        if (currentHealth <= 0)
            Die();
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
        DropLoot();
        CombatEvents.EnemyDied(this);
        // combat events shouldhelp trigger loot drops from here maybe
        Destroy(gameObject);
    }

    void DropLoot()
    {
        Item item = dropTable.GetDrop();

        if (item != null)
        {
            PickUpItem instance = Instantiate(pickUpItem,transform.position, Quaternion.identity);
            instance.ItemToPick = item;
        }

    }

}
