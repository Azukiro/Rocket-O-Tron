using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBehaviour : MonoBehaviour
{
    /**
     * Private Serialize Fields
    **/
    [SerializeField] private Animator dragonMovement;

    /**
     * Functions
    **/

    private void OnTriggerEnter(Collider other)
    {
        //If player enter dragon's trigger
        if (other.gameObject.CompareTag("Player"))
        {
            //Enable dragon's animation
            dragonMovement.enabled = true;
        }
    }
}
