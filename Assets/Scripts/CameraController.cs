using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float PlayerCameraDistance { get; set; }

    public Transform cameraTarget;

    Camera playerCamera;

    float zoomSpeed = 25f;

    float rotation = 0f;
    // Use this for initialization
    void Start () {

        PlayerCameraDistance = 7f;
        playerCamera = GetComponent<Camera>();


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
        // update position
        transform.position = new Vector3
           (cameraTarget.position.x ,
            cameraTarget.position.y + PlayerCameraDistance, 
            cameraTarget.position.z + PlayerCameraDistance );


	}
}
