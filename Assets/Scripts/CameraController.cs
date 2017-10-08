using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

    public float PlayerCameraDistance { get; set; }

    public Transform cameraTarget;

    Camera playerCamera;

    float zoomSpeed = 25f;

    //float rotation = 0f;

    public Vector3 RotOffset;

    Vector3 ControllPoint;

    bool Flipside;


    // Use this for initialization
    void Start () {
        Flipside = false;
        PlayerCameraDistance = 7f;
        playerCamera = GetComponent<Camera>();

        //        RotOffset = Vector3.zero;
        RotOffset = new Vector3(PlayerCameraDistance, 0, 0);//- PlayerCameraDistance,- PlayerCameraDistance);

	}
	
	// Update is called once per frame
	void Update () {




        // axis name stems from edit/project settings/input/axis
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {

            // field of view effect cheats zoom without camera move
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            playerCamera.fieldOfView -= scroll * zoomSpeed;


            // clamp field of view zoom
            if ( playerCamera.fieldOfView < 20)
            {
                playerCamera.fieldOfView = 20;
            }

            if (playerCamera.fieldOfView > 100)
            {
                playerCamera.fieldOfView = 100;
            }

        }


        //      if (Input.GetKeyDown(KeyCode.LeftArrow))
        //      {
        //          //     transform.Rotate(Vector3.up * Time.deltaTime * 100);
        //          //      transform.Rotate(Vector3.right * Time.deltaTime * -675);
        //          //transform.rotation = new Quaternion
        //          //    (transform.rotation.x,
        //          //    transform.rotation.y,
        //          //    transform.rotation.z,
        //          //    transform.rotation.w + 0.2f);

        //          // transform.rotation.z += 0.1f;


        //          float tiltAroundZ = 5f;
        ////          float tiltAroundX = 0;
        //          Quaternion target = Quaternion.Euler(transform.rotation.x, 0, tiltAroundZ);
        //          transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 0.2f);


        //          //           transform.rotation = Quaternion.Euler(new Vector3(0, 30, 0));

        //      }

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    transform.Rotate(Vector3.down * Time.deltaTime * 100);
        //    transform.Rotate(Vector3.right * Time.deltaTime * 100);


        //}



        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Flipside)
            RotOffset = new Vector3(RotOffset.x + 0.2f, RotOffset.y, RotOffset.z);
            else
            RotOffset = new Vector3(RotOffset.x - 0.2f, RotOffset.y, RotOffset.z);

            CheckFlip();
            GetNewY();
            Debug.Log("new rot offset x" + RotOffset.x + " y" + RotOffset.y + " z" + RotOffset.z  );
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Flipside)
                RotOffset = new Vector3(RotOffset.x - 0.2f, RotOffset.y, RotOffset.z);
            else
                RotOffset = new Vector3(RotOffset.x + 0.2f, RotOffset.y, RotOffset.z);
          
            CheckFlip();
            GetNewY();
            Debug.Log("new rot offset x" + RotOffset.x + " y" + RotOffset.y + " z" + RotOffset.z);
        }



        ////base of funtion to the corner so you dont need to try and root a negatave int
        ControllPoint = new Vector3(cameraTarget.position.x - PlayerCameraDistance,
                                    cameraTarget.position.y + PlayerCameraDistance,
                                    cameraTarget.position.z + PlayerCameraDistance);


        // update position
        //transform.position = new Vector3
        //   (cameraTarget.position.x + RotOffset.x,
        //    cameraTarget.position.y + PlayerCameraDistance,
        //    cameraTarget.position.z + PlayerCameraDistance + RotOffset.z);
               transform.position = ControllPoint + RotOffset;


    }

    private void CheckFlip()
    {
        if (RotOffset.x <= 0 || RotOffset.x >= (PlayerCameraDistance * 2))
        {
            Debug.Log("FLIP CAM");
            if (Flipside)
                Flipside = false;
            else
                Flipside = true;
        }
    }

    private void GetNewY()
    {
        // see the formula of function of a circle as a line
        //float xSquared = RotOffset.x * RotOffset.x;
        //float newY = (float)Math.Round((double)Math.Sqrt((double)((PlayerCameraDistance * PlayerCameraDistance) - xSquared)));
        float newY;
        //(float)Math.Sqrt((double)(49 - xSquared));

        if (Flipside)
        {
            float xSquared = (RotOffset.x) * RotOffset.x;
            newY = (float)Math.Round((double)Math.Sqrt((double)((PlayerCameraDistance * PlayerCameraDistance) - xSquared)));


            newY = newY + PlayerCameraDistance;
        }
        else
        {
            float xSquared = RotOffset.x * RotOffset.x;
            newY = (float)Math.Round((double)Math.Sqrt((double)((PlayerCameraDistance * PlayerCameraDistance) - xSquared)));
        }

        Debug.Log("new cam points  X" + RotOffset.x + "Y" + newY);

        RotOffset = new Vector3(RotOffset.x, newY, RotOffset.z);


    }


}
