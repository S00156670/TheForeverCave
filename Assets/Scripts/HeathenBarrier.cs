
using UnityEngine;
using System.Collections;
using System;

public class HeathenBarrier : Interactable, IEnemy
{
   // public LayerMask aggroLayerMask;
    //  public float power;
    //   public float toughness;
    //private float maxHealth;
    //private float currentHealth;

   // private UnityEngine.AI.NavMeshAgent navAgent;
  //  public CharachterStats charachterStats;

  //  Collider[] aggroNavTargets;

  //  Player player;

    public int Experience { get; set; }

 //   public DropTable dropTable { get; set; }

 //   public PickUpItem pickUpItem;

    // Use this for initialization
    void Start()
    {
        Experience = 20;
    }


    public void PerformAttack()
    {}

    public virtual void TakeDamage(int amount)
    {}

    public virtual void TakeDamage(int amount, Damage.DamageType type)
    {
        if (type == Damage.DamageType.Divine)
        {
            Destroy(gameObject);
        }
        Debug.Log(type.ToString() + " damage recieved");
        Debug.Log(this.name + " revieved " + amount + " damage");
    }


    // FixedUpdate is called less often than update but still often enough for smooth play
    void FixedUpdate()
    {

        //// chech player range
        ////   Physics.OverlapSphere();

        //aggroNavTargets = Physics.OverlapSphere(transform.position,
        //                    /*line of sight*/10,
        //                    aggroLayerMask);

        //if (aggroNavTargets.Length > 0)
        //{
        //    //          Debug.Log("enemy has spotted a player");
        //    ChasePlayer(aggroNavTargets[0].GetComponent<Player>());
        //}


    }



    public void Die()
    {

     //   CombatEvents.EnemyDied(this);
        // combat events shouldhelp trigger loot drops from here maybe
        Destroy(gameObject);
    }



}



//using UnityEngine;
//using System.Collections;
//using System;

//public class HeathenBarrier : Enemy {


//    public override void TakeDamage(int amount)
//    {
//        Debug.Log(this.name + " revieved " + amount + " damage");
//    }

//    public override void TakeDamage(int amount, Damage.DamageType type)
//    {
//        Debug.Log(type.ToString() + " damage recieved");
//        Debug.Log(this.name + " revieved " + amount + " damage");

//        if (type == Damage.DamageType.Divine)
//        {
//            Destroy(gameObject);
//        }

//    }

//}
