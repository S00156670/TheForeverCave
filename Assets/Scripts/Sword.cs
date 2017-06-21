using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Sword : MonoBehaviour , IWeapon {
    public List<BaseStat> Stats { get; set; }

    public void PerformAttack()
    {
        Debug.Log("Sword attack");
    }

 //   // Use this for initialization
 //   void Start () {
	
	//}
	
	//// Update is called once per frame
	//void Update () {
	
	//}
}
