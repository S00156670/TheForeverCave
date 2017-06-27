using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Staff : MonoBehaviour , IWeapon , IProjectileWeapon {
    public List<BaseStat> Stats { get; set; }

    public Transform ProjectileSpawn { get; set; }

    Fireball fireball;

    private Animator anim;

        
    private void Start()
    {
        fireball = Resources.Load<Fireball>("Weapons/Projectiles/fireball");
        anim = GetComponent<Animator>();
    }

    public void PerformAttack()
    {



        anim.SetTrigger("Base_Attack");
        Debug.Log(this.name + " attack has triggered" );

   //     CastProjectile();

    }

    public void PerformSpecialAttack()
    {
        anim.SetTrigger("Base_Attack"); // temp, add 2nd animation later
        Debug.Log(this.name + " special attack has triggered");
    }



    public void CastProjectile()
    {
        Debug.Log("Generating projectile");
        // for arrows will need to make rotation same as player rotation
        Fireball fireballInstance = (Fireball)Instantiate(fireball, ProjectileSpawn.position, transform.rotation);
        // get foreward vector of projectile spawn
        fireballInstance.Direction = ProjectileSpawn.forward;

    }


}
