using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour {

    float rotStage;

	// Use this for initialization
	void Start () {
        rotStage = 0;
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (rotStage <= 0)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * -450);
                GameObject.Find("Main Camera").GetComponent<CameraController>().RotOffset += new Vector3(-1, 0, 1);
                rotStage++;
                Debug.Log("Rotation stage " + rotStage);
            }
            else if (rotStage <= 5)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * -450);
                GameObject.Find("Main Camera").GetComponent<CameraController>().RotOffset += new Vector3(-1, 0, -1);
                rotStage++;
                Debug.Log("Rotation stage " + rotStage);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (rotStage >= 0)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 450);
                GameObject.Find("Main Camera").GetComponent<CameraController>().RotOffset += new Vector3(1, 0, 1);
                rotStage--;
                Debug.Log("Rotation stage " + rotStage);
            }
            else if (rotStage >= -5)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 450);
                GameObject.Find("Main Camera").GetComponent<CameraController>().RotOffset += new Vector3(1, 0, -1);
                rotStage--;
                Debug.Log("Rotation stage " + rotStage);
            }

            //      transform.Rotate(Vector3.right * Time.deltaTime * -675);
            //transform.rotation = new Quaternion
            //    (transform.rotation.x,
            //    transform.rotation.y,
            //    transform.rotation.z,
            //    transform.rotation.w + 0.2f);
            // transform.rotation.z += 0.1f;
            //float tiltAroundZ = 5f;
            ////          float tiltAroundX = 0;
            //Quaternion target = Quaternion.Euler(transform.rotation.x, 0, tiltAroundZ);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 0.2f);
            //           transform.rotation = Quaternion.Euler(new Vector3(0, 30, 0));

        }

        if (rotStage == 0)
        {
            Reset();
        }
    }
    private void Reset()
    {
        GameObject.Find("Main Camera").GetComponent<CameraController>().RotOffset = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

}
