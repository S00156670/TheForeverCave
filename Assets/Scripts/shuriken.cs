using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken : MonoBehaviour, IWeapon, IProjectileWeapon
{

    public List<BaseStat> Stats { get; set; }

    public int CurrentDamage { get; set; }

    public Transform ProjectileSpawn { get; set; }

    public bool IsMagic { get; set; }

    shurikenProjectile shurikenProjectile;

    private Animator anim;


    private void Start()
    {
        shurikenProjectile = Resources.Load<shurikenProjectile>("Weapons/Projectiles/shurikenProjectile");
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
        shurikenProjectile shurikenProjectileInstance = (shurikenProjectile)Instantiate(shurikenProjectile, ProjectileSpawn.position, transform.rotation);
        // get foreward vector of projectile spawn point
        shurikenProjectileInstance.Direction = ProjectileSpawn.forward;

        GetComponent<Player>().charachterStats.RemoveStatBonus(gameObject.GetComponent<IWeapon>().Stats);
        Destroy(gameObject);

    }

}
