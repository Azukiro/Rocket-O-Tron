using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyRay
{
    public Vector3 Origin;

    public Vector3 Destination;

    public float Range;
}

public class EnnemiesMovement : MonoBehaviour
{
    private Rigidbody _Rigidbody;

    private Transform _Transform;

    public bool MakeRotation;

    public float Direction = 1;

    public bool PlayerDetect;

    public float OldDirection = 1;

    [Header("Movement")]
    [SerializeField]
    private float _TranslationSpeed;

    private void Awake()
    {
        _Transform = GetComponent<Transform>();
        _Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (MakeRotation)
        {
            _Transform.GetChild(0).rotation *= Quaternion.Euler(0, 180, 0);

            MakeRotation = false;
        }
    }

    private void FixedUpdate()
    {
        if (!MakeRotation)
        {
            Vector3 newVelocity = Direction * _Transform.right * _TranslationSpeed;
            if (PlayerDetect)
            {
                newVelocity *= 2;
            }
            Vector3 velocityChange = newVelocity - _Rigidbody.velocity;
            _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}