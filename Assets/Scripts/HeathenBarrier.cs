using UnityEngine;
using System.Collections;
using System;

public class HeathenBarrier : Interactable, IEnemy
{

    public int Experience { get; set; }

 //   public DropTable dropTable { get; set; }
 //   public PickUpItem pickUpItem;

    // Use this for initialization
    void Start()
    {
        Experience = 30;
    }

    public void PerformAttack()
    {}

    public virtual void TakeDamage(int amount)
    {}

    public virtual void TakeDamage(int amount, Damage.DamageType type)
    {
        // weakness against divine energy
        if (type == Damage.DamageType.Divine)
        {
            Die();
        }
        Debug.Log(type.ToString() + " damage recieved");
        Debug.Log(this.name + " revieved " + amount + " damage");
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}

