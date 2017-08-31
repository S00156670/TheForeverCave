using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float PlayerCameraDistance { get; set; }

    public Transform cameraTarget;

    Camera playerCamera;

    float zoomSpeed = 25f;

    //float rotation = 0f;

    public Vector3 RotOffset;


    // Use this for initialization
    void Start () {

        PlayerCameraDistance = 7f;
        playerCamera = GetComponent<Camera>();

        RotOffset = Vector3.zero;


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

        // update position
        transform.position = new Vector3
           (cameraTarget.position.x + RotOffset.x,
            cameraTarget.position.y + PlayerCameraDistance, 
            cameraTarget.position.z + PlayerCameraDistance + RotOffset.z);


	}
}
