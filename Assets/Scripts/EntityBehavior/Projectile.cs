using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    [Tooltip("Position we want to hit")]
    public Vector3 targetPos;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 10;

    private Vector3 startPos;

    private void Start()
    {
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = transform.position;
    }

    private void Update()
    {
    }

    private void Arrived()
    {
        Destroy(gameObject);
    }

    ///
    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
    /// that makes the local +X axis point in the given forward direction.
    ///
    /// forward direction
    /// Quaternion that rotates +X to align with forward
}