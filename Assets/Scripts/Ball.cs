using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Ball : MonoBehaviour, IWeapon, IProjectileWeapon
{
    public List<BaseStat> Stats { get; set; }

    public int CurrentDamage { get; set; }

    public Transform ProjectileSpawn { get; set; }

    public bool IsMagic { get; set; }

    BallProjectile ballProjectile;

    private Animator anim;


    private void Start()
    {
        ballProjectile = Resources.Load<BallProjectile>("Weapons/Projectiles/ballProjectile");
        anim = GetComponent<Animator>();
        IsMagic = true;
    }

    public void PerformAttack(int damage)
    {



        anim.SetTrigger("Base_Attack");
        Debug.Log(this.name + " attack has triggered");

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
        BallProjectile ballProjectileInstance = (BallProjectile)Instantiate(ballProjectile, ProjectileSpawn.position, transform.rotation);
        // get foreward vector of projectile spawn point
        ballProjectileInstance.Direction = ProjectileSpawn.forward;

    }


}
