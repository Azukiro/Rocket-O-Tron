using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesMovement : MonoBehaviour
{
    private Rigidbody _Rigidbody;

    private Transform _Transform;

    [Header("Movement")]
    [SerializeField]
    private float _TranslationSpeed;

    [Header("RaycastDetection")]
    [SerializeField]
    private LayerMask _ObjectToDetect;

    [SerializeField]
    private List<Vector3> _DetectDirections;

    [SerializeField]
    private int _MaxDetectionDistance;

    private void Awake()
    {
        _Transform = GetComponent<Transform>();
        _Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
    }

    private bool makeRotation;

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    if (!makeRotation)
        //    {
        //        makeRotation = true;
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!makeRotation)
            {
                makeRotation = true;
            }
        }
    }

    private bool PlayerDetect;

    private void Update()
    {
        foreach (Vector3 vector in _DetectDirections)
        {
            if (Physics.Raycast(new Ray(_Transform.position, _Transform.TransformDirection(vector)), out RaycastHit result, _MaxDetectionDistance, 1 << _ObjectToDetect))
            {
                Debug.Log("Detected Player" + result);
                PlayerDetect = true;
            }
        }

        if (makeRotation)
        {
            _Transform.rotation *= Quaternion.Euler(0, 180, 0);

            makeRotation = false;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 vector in _DetectDirections)
        {
            Gizmos.color = Color.red;
            Vector3 direction = _Transform.TransformDirection(vector) * _MaxDetectionDistance;

            Gizmos.DrawRay(_Transform.position, direction);
        }
    }

    private void FixedUpdate()
    {
        if (!makeRotation)
        {
            Vector3 newVelocity = _Transform.right * _TranslationSpeed;
            Vector3 velocityChange = newVelocity - _Rigidbody.velocity;
            _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}