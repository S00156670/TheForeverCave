using UnityEngine;
using System.Collections;

public class WorldInteraction : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent playerAgent;
    GameObject player;

    // Use this for initialization
    void Start()
    {// set navmesh
        playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
     //   player = GetCom
    }

    // Update is called once per frame
    void Update()
    {
        // set up wasd option
       // if (Input.GetKeyDown("W"))
       // {
       ////     playerAgent.destination = Player
       // }
                                            // check to make sure we are not clicking in UI
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            GetInteraction();

        // ensure look direction for Rclick to assist aiming
        if (Input.GetMouseButton(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            // get click point
            Vector3 clickPoint = Vector3.zero;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("The ray hit at: " + hit.point);

                clickPoint = hit.point;
            }
            // set looking at
            playerAgent.updateRotation = false;
            Vector3 lookDirection = new Vector3(clickPoint.x, playerAgent.transform.position.y, clickPoint.z);
            playerAgent.transform.LookAt(lookDirection);
            playerAgent.updateRotation = true;
        }

    }

    void GetInteraction()
    {
        // set up ray for click
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;


        // isthis ray hittig cave mesh properly?
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {   // itterate a temporary copy of interacted object
            GameObject interactedObject = interactionInfo.collider.gameObject;

            if (interactedObject.tag == "Interactible Object")
            {
             //   Debug.Log("interaction  click");
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);

            }
            else if (interactedObject.tag == "Enemy")
            {
                   Debug.Log("enemy click");
                // should get<Enemy>()?
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else
            {
                Debug.Log("destination  click : " + "x:" + interactionInfo.point.x + " y:" + interactionInfo.point.y + " z:" + interactionInfo.point.z);
                playerAgent.stoppingDistance = 0f;
                playerAgent.destination = interactionInfo.point;

            }
        }
    }

}
