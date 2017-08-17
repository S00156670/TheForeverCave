﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Portal : MonoBehaviour{

    public bool active { get; set; }
    public bool triggered;// { get; set; }
    public float triggerRadius;
    public Vector3 destination { get; set; }

    // Use this for initialization
    void Start()
    {
        triggered = false;
        active = true;


        // debug set
        triggerRadius = 5;
        destination = new Vector3(30.148,200);

    }

    //FixedUpdate is called less often than update but still often enough for smooth play
    //void FixedUpdate()
    //{

    //}

    public float CheckDistance(Vector3 playerPos)
    {
        //float x = gameobject.transform.position.x - playerPos.x;
        //if (x < 0)
        //    x = (x * -1);

        //float y = gameobject.transform.position.y - playerPos.y;
        //if (y < 0)
        //    y = (y * -1);

        //float z = gameobject.transform.position.z - playerPos.z;
        //if (z < 0)
        //    z = (z * -1);

       // Vector3 dist = new Vector3(x,y,z);

        float dist = Vector3.Distance(playerPos, transform.position);
        

        if (dist < triggerRadius && active == true && triggered == false)
            triggered = true;

        if (dist > triggerRadius )
            triggered = false;//reset


        //   return dist;

    }

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
