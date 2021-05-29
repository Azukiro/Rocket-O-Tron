using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyIaDirection : MonoBehaviour
{
    private Rigidbody _Rigidbody;

    private Transform _Transform;

    [SerializeField]
    private float _TranslationSpeed;

    [SerializeField]
    private float _RotationSpeed;

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
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!makeRotation)
            {
                makeRotation = true;
            }
        }
    }

    //private float oldRotate;

    private void FixedUpdate()
    {
        if (makeRotation)
        {
            _Transform.rotation *= Quaternion.Euler(0, 180, 0);

            makeRotation = false;

            //oldRotate = _Transform.rotation.eulerAngles.y;
            //Try Smooth Rotation need to be remove
            /*
            Vector3 newAngularVelocity = _Rigidbody.transform.up * 1 * Mathf.Deg2Rad * _RotationSpeed;
            Vector3 angularVelocityChange = newAngularVelocity - _Rigidbody.angularVelocity;

            //Debug.Log(hInput);
            _Rigidbody.AddTorque(angularVelocityChange, ForceMode.VelocityChange);
            if (_Transform.rotation.eulerAngles.y >= oldRotate + 180)
            {
                Debug.Log(_Transform.rotation.eulerAngles.y);

                Debug.Log(_Transform.rotation.eulerAngles.y - 180 + " 33");
                _Transform.rotation *= Quaternion.Euler(0, -_Transform.rotation.eulerAngles.y - 180, 0);
                Debug.Log(_Transform.rotation.eulerAngles.y + "     2");
                makeRotation = false;
            }*/
        }
        else
        {
            Vector3 newVelocity = _Transform.right * _TranslationSpeed;
            Vector3 velocityChange = newVelocity - _Rigidbody.velocity;
            _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}