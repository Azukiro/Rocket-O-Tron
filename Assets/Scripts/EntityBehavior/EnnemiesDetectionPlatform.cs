using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesDetection : MonoBehaviour
{
    private EnnemiesMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<EnnemiesMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!_movement.MakeRotation)
            {
                _movement.MakeRotation = true;
                _movement.Direction *= -1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!_movement.MakeRotation)
            {
                _movement.MakeRotation = true;
                _movement.Direction *= -1;
            }
        }
    }
}