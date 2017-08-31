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

    public bool IsBoss;

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
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetEnemyData();

    }


    void SetEnemyData()
    {
        Experience = 20;
        int level = GameObject.Find("DungeonGenerator").GetComponent<DungeonManager>().levelStage;

        if (IsBoss)
        {
            Debug.Log(("### LevelBoss ##"));
            charachterStats = new CharachterStats((level * 3), 2, (5 + level), (level * 8), 5, 4, 2);
            Experience = 50;
        }
        else
        {
            charachterStats = new CharachterStats((level * 2), 1, 2 + level, (level * 7), 5, 4, 2);
        }
        //        charachterStats = new CharachterStats((level * 2), 1, 4, (level * 7), 5, 4, 2);
        //     charachterStats = new CharachterStats(9, 10, 2);
        //    charachterStats = new CharachterStats(3, 1, 2, 5, 7, 4, 2);

        maxHealth = charachterStats.GetStat(BaseStat.BaseStatType.Health).GetCalculatedStatValue() * 10;
        currentHealth = maxHealth;

        dropTable = new DropTable();
        //dropTable.loot = new System.Collections.Generic.List<LootDrop>
        //                {
        //                    new LootDrop("sword",25),
        //                    new LootDrop("staff",25),
        //                    new LootDrop("potion",25),
        //                    new LootDrop("ball",25),
        //                };
        switch (level)
        {
            case 1:
                dropTable.loot = new System.Collections.Generic.List<LootDrop>
                        {
                            new LootDrop("potion",50),
                            new LootDrop("shuriken",2),
                        };
                break;
            case 2:
                dropTable.loot = new System.Collections.Generic.List<LootDrop>
                        {
                            new LootDrop("potion",45),
                            new LootDrop("staff",15),
                            new LootDrop("shuriken",2),
                        };
                break;
            case 3:
                dropTable.loot = new System.Collections.Generic.List<LootDrop>
                        {
                            new LootDrop("potion",40),
                            new LootDrop("staff",20),
                            new LootDrop("sword",10),
                            new LootDrop("shuriken",5),
                        };
                break;
            case 4:
                dropTable.loot = new System.Collections.Generic.List<LootDrop>
                        {
                            new LootDrop("potion",30),
                            new LootDrop("staff",20),
                            new LootDrop("sword",25),
                            new LootDrop("shuriken",5),
                        };
                break;
            case 5:
                dropTable.loot = new System.Collections.Generic.List<LootDrop>
                        {
                            new LootDrop("potion",40),
                            new LootDrop("staff",10),
                            new LootDrop("sword",10),
                            new LootDrop("shuriken",5),
                        };
                break;
            default:
                dropTable.loot = new System.Collections.Generic.List<LootDrop>
                        {
                            new LootDrop("potion",20),
                        };
                break;
        }

        // debug data
        Debug.Log("### Stat Check for level " + level);
        foreach (BaseStat s in charachterStats.stats)
        {
            Debug.Log(" : " + s.StatName + " : " + s.BaseValue);
        }
        Debug.Log(" : : max health is " + maxHealth);
        Debug.Log("############################");

    }

    public void RenewHealth()
    {
        maxHealth = charachterStats.GetStat(BaseStat.BaseStatType.Health).GetCalculatedStatValue() * 10;

        currentHealth = maxHealth;

        Debug.Log("Stat Check");
        foreach (BaseStat s in charachterStats.stats)
        {
            Debug.Log(" : " + s.StatName + " : " + s.BaseValue);
        }
        Debug.Log(" : : max health is " + maxHealth);
    }


    public void PerformAttack()
    {
        int damage = charachterStats.GetStat(BaseStat.BaseStatType.MeleeSkill).GetCalculatedStatValue();
        player.TakeDamage(damage);
        Debug.Log(this.name + "is attacking player for " + damage);
    }

    public virtual void TakeDamage(int amount)
    {
        amount -= charachterStats.GetStat(BaseStat.BaseStatType.Toughness).GetCalculatedStatValue();

        if (amount > 0)
            currentHealth = currentHealth - amount;

        Debug.Log(this.name + " revieved " + amount + " current health is " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public virtual void TakeDamage(int amount , Damage.DamageType type)
    {
        Debug.Log(type.ToString() + " damage recieved");

        amount -= charachterStats.GetStat(BaseStat.BaseStatType.Toughness).GetCalculatedStatValue();

        if (amount > 0)
            currentHealth = currentHealth - amount;

        Debug.Log(this.name + " revieved " + amount + " current health is " + currentHealth);

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



//public class Rat : Enemy
//{

//    private void Start()
//    {
//        dropTable = new DropTable();
//        dropTable.loot = new System.Collections.Generic.List<LootDrop>
//                        {
//                            new LootDrop("potion",100)
//                        };

//  //      Experience = 20;
// //       navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
//        //     charachterStats = new CharachterStats(9, 10, 2);
//        charachterStats = new CharachterStats(10, 1, 2, 5, 7, 4, 2);
//        RenewHealth();
//    }

//}