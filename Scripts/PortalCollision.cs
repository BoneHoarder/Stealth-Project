using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalCollision : MonoBehaviour
{   
    [SerializeField] private UnityEvent whenPlayerWins;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){

        if (other.CompareTag("Player"))
        {
            //Debug.Log("You won!");

            whenPlayerWins.Invoke();
        }
    }
}
