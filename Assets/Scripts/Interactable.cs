using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    [HideInInspector]


    public NavMeshAgent playerAgent;
    private bool hasInteracted;
    bool IsEnemy;


    public virtual void MoveToInteraction(NavMeshAgent playerAgent)
    {
        IsEnemy = gameObject.tag == "Enemy";

        hasInteracted = false;
        // get the reference for this instance of interaction
        this.playerAgent = playerAgent;
        // dont go to merge through target
        playerAgent.stoppingDistance = 1.5f;
        // sent charachter to interaction point
        playerAgent.destination = this.transform.position;
        // trigger interaction action, will require a distance check to stall action
     //   Interact();

    }



    public virtual void Interact()
    {
        Debug.Log("interacting with base interaction class");
    }

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    void Update()
    {
        // check if still traveling to interaction 
        if (!hasInteracted && playerAgent != null && !playerAgent.pathPending)
        {// distance between player agent and destination
            if (playerAgent.remainingDistance < playerAgent.stoppingDistance)
            {
                if (!IsEnemy)
                {Interact(); }    

                EnsureLookDirection();
                hasInteracted = true;
            }
        }
    }
    void EnsureLookDirection()
    {
        playerAgent.updateRotation = false;
        Vector3 lookDirection = new Vector3(transform.position.x , playerAgent.transform.position.y , transform.position.z);
        playerAgent.transform.LookAt(lookDirection);
        playerAgent.updateRotation = true;
    }

}
