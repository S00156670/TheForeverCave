using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Sword : MonoBehaviour , IWeapon {
    public List<BaseStat> Stats { get; set; }

    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
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
    }

    //   // Use this for initialization
    //   void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
