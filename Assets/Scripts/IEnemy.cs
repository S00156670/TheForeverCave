using UnityEngine;
using System.Collections;

public interface IEnemy  {

    int Experience { get; set; }

    void TakeDamage(int amount);
    void TakeDamage(int amount, Damage.DamageType type);
   
    void PerformAttack();

    void Die();

}
