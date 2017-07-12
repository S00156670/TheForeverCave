using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Sword : MonoBehaviour , IWeapon {
    public List<BaseStat> Stats { get; set; }

    public int CurrentDamage { get; set; }

    private Animator anim;

    // 2 reasons for this
     // - throw weapon
     // - easier to share anim with ranged if this is present
    void CastProjectile()
    { }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;
        anim.SetTrigger("Base_Attack");
        Debug.Log(this.name + " attack has triggered" );
    }

    public void PerformSpecialAttack()
    {
        anim.SetTrigger("Base_Attack"); // temp, add 2nd animation later
        Debug.Log(this.name + " special attack has triggered");
    }

    // check for weapon collision collision
    /*private*/ void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " hit : " + other.name +" " + other.ToString());

        if (other.tag == "Enemy")
        {
            // this will need to be better and make sure its getting the right stat
            // currently just damage of sword but should be plus player melee skill
  //          other.GetComponent<IEnemy>().TakeDamage(Stats[0].GetCalculatedStatValue());
            other.GetComponent<IEnemy>().TakeDamage(CurrentDamage);
        }

    }



    //   // Use this for initialization
    //   void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
