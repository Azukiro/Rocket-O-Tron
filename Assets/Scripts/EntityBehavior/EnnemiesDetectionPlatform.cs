using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesDetectionPlatform : MonoBehaviour
{
    private EnnemiesMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<EnnemiesMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.CompareTag("Wall"))
        {
            if (!_movement.MakeRotation)
            {
                _movement.MakeRotation = true;
                _movement.Direction *= -1;
            }
        }
    }
}