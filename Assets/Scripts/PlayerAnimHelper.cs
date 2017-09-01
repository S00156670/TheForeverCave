using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimHelper : MonoBehaviour {

    static Animator anim;

	// Use this for initialization
	void Start ()
    {
		    anim = GameObject.Find("brother").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("switching to walk animation");
            anim.SetTrigger("isMoving");

            if (anim = null)
            {
                Debug.Log("cant find anim component");
            }

        }
    }
}
