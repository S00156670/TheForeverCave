﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public bool active { get; set; }
    public bool triggered;// { get; set; }
    public float triggerRadius;
    public Vector3 destination;//{ get; set; }
 //   public Vector3 position;

    // Use this for initialization
    void Start()
    {
        triggered = false;
        active = true;

        // debug set
        triggerRadius = 5;
  //      destination = new Vector3(12.6f, 4, 32.1f); //new Vector3(30, 148, 200);
    }



    public void CheckDistance(Vector3 playerPos)
    {
        float dist = Vector3.Distance(playerPos, transform.position);

        if (dist < triggerRadius && active == true && triggered == false)
        {
            triggered = true;
            Debug.Log("Portal has been triggered, destination: " + destination.x + "|" + destination.y + "|" + destination.z);
        }

        if (dist > triggerRadius || active == false )
            triggered = false;//reset

    }


    //public Portal()
    //{
    //    triggered = false;
    //    active = true;

    //    // debug set
    //    position = new Vector3(37,147,195);
    //    triggerRadius = 5;
    //    destination = new Vector3(30, 148, 200);
    //}


    ////   FixedUpdate is called less often than update but still often enough for smooth play
    //   void FixedUpdate()
    //   {
    //       if (CheckDistance(Player.pos) > triggerRadius)
    //       {
    //       }
    //   }

}


//public class Portal : MonoBehaviour {

//    public bool active { get; set; }
//    public bool triggered;// { get; set; }
//    Collider[] NavTargets;
//    public LayerMask portalRange;
//    public Vector3 destination { get; set; }

//    // Use this for initialization
//    void Start () {
//        triggered = false;
//        active = true;
//	}


//    // FixedUpdate is called less often than update but still often enough for smooth play
//    void FixedUpdate()
//    {

//        NavTargets = Physics.OverlapSphere(transform.position,
//                            /*line of sight*/5,
//                            portalRange);
//        if (aggroNavTargets.Length > 0)
//        {
//            foreach (var item in collection)
//            {

//            }

//            Debug.Log("Player interacting with portal");
//        }


//    }
//}
