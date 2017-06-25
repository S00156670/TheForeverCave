using UnityEngine;
using System.Collections;

public class WorldInteraction : MonoBehaviour
{

    NavMeshAgent playerAgent;
    GameObject player;

    // Use this for initialization
    void Start()
    {// set navmesh
        playerAgent = GetComponent<NavMeshAgent>();
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
    }

    void GetInteraction()
    {
        // set up ray for click
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;



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
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);

            }
            else
            {
                Debug.Log("destination  click");
                playerAgent.stoppingDistance = 0f;
                playerAgent.destination = interactionInfo.point;

            }
        }
    }

}
