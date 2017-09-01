using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimHelper : MonoBehaviour {

    static Animator anim;
    Vector3 previousPos;

	// Use this for initialization
	void Start ()
    {
            previousPos = GameObject.Find("Player").transform.position;
            anim = GameObject.Find("brother").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (previousPos != GameObject.Find("Player").transform.position)
        {
            anim.SetBool("isMoving", true);
            previousPos = GameObject.Find("Player").transform.position;
         //   Debug.Log("walking");
        }
        else
        {
            anim.SetBool("isMoving", false);
         //   Debug.Log("idle");
        }

    }
}
