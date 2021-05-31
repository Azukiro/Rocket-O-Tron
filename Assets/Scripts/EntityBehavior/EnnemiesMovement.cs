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
        if (makeRotation)
        {
            //_Transform.rotation = Quaternion.RotateTowards(_Transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 1000);
            _Transform.rotation *= Quaternion.Euler(0, 180, 0);

            makeRotation = false;
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