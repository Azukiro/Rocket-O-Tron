using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class FinalDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // End of a game
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.Raise(new GamePlayerInExitDoorEvent());
            Debug.Log("Win");
        }
    }
}