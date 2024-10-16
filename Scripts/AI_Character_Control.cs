using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public enum AICharacterControlState{
    Patrolling,
    Chasing
}

public class AI_Character_Control : MonoBehaviour
{   
    [SerializeField] private bool debug;
    [SerializeField] private AICharacterControlState state;

    public Transform playerTransform;

    private NavMeshAgent navMeshAgent; 

    public Transform waypointGroup;

    private Transform currentWaypoint;

    [SerializeField ]private float maxVisibilityDistance= 10f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent= GetComponent<NavMeshAgent>();

        currentWaypoint= SelectDestination();

        navMeshAgent.SetDestination(currentWaypoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        // navMeshAgent.SetDestination(playerTransform.position);

        switch (state)
        {
            case AICharacterControlState.Patrolling:

                if(CheckVisibility())
                {
                    state=AICharacterControlState.Chasing;
                }
                break;
            case AICharacterControlState.Chasing:

                navMeshAgent.SetDestination(playerTransform.position);

                if(!CheckVisibility()){

                    state=AICharacterControlState.Patrolling;
                    navMeshAgent.SetDestination(currentWaypoint.position);

                }

                break;

        }

    }

    void OnTriggerEnter(Collider other){
        if(other.transform== currentWaypoint){
            
            currentWaypoint= SelectDestination();

            navMeshAgent.SetDestination(currentWaypoint.position);
        }
    }

    Transform SelectDestination(){

        int index= Random.Range(0, waypointGroup.childCount);

        Transform newWaypoint= waypointGroup.GetChild(index);

        while(newWaypoint==currentWaypoint){
            
            index= Random.Range(0, waypointGroup.childCount);

            newWaypoint= waypointGroup.GetChild(index);

        }

        return newWaypoint;
    }

    bool CheckVisibility(){

        RaycastHit hit;

        if(Physics.Raycast(transform.position + Vector3.up, playerTransform.position - transform.position, out hit, maxVisibilityDistance)){

            //Debug.Log($"Raycast hit something : {hit.collider.name}");

            if(hit.collider.CompareTag("Player")){
                if(Vector3.Angle(transform.forward, playerTransform.position - transform.position)< 45){
                    return true;
                }
            }
        }

        return false;
    }
}
