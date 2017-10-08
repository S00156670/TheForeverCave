using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour {



    bool Flipside;

	// Use this for initialization
	void Start () {
        Flipside = false;
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + 2f , transform.position.y, transform.position.z);
            CheckFlip();
            GetNewY();
            UpdateCam();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);
            CheckFlip();
            GetNewY();
            UpdateCam();
        }

    }



    private void CheckFlip()
    {
        if (transform.position.x == 7 || transform.position.x == -7)
        {
            if (Flipside)
                Flipside = false;
            else
                Flipside = true;
        }
    }

    private void GetNewY()
    {
        // see the formula of function of a circle as a line
        float xSquared = transform.position.x * transform.position.x;
        float newY = (float)Math.Round((double)Math.Sqrt((double)(49 - xSquared)));

        //(float)Math.Sqrt((double)(49 - xSquared));

        if (Flipside)
        {
            newY = -newY;
        }

        Debug.Log("new cam points  X"+ transform.position.x + "Y" + newY);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);


    }

    private void UpdateCam()
    {
      //  GameObject.Find("Main Camera").GetComponent<CameraController>().RotOffset += new Vector3(-rotStage, 0, -rotStage);
    }


}
