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

    [Header("Attck")]
    [SerializeField]
    private int _Range;

    private void Awake()
    {
        _Transform = GetComponent<Transform>();
        _Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
    }

    private bool makeRotation;

    private float direction = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!makeRotation)
            {
                makeRotation = true;
                direction *= -1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision" + direction);
            if (!makeRotation)
            {
                makeRotation = true;
                direction *= -1;
            }
            Debug.Log("Collision" + direction);
        }
    }

    private bool PlayerDetect;

    private float oldDirection = 1;

    private void Update()
    {
        PlayerDetect = false;

        foreach (Vector3 vector in _DetectDirections)
        {
            Vector3 vectorTransform = _Transform.TransformDirection(vector);
            if (Physics.Raycast(new Ray(_Transform.position, vectorTransform), out RaycastHit result, _MaxDetectionDistance, _ObjectToDetect))
            {
                //Debug.Log(Mathf.Abs(_Transform.position.x) + _Range + "Detected Player" + Mathf.Abs(result.transform.position.x));
                if (Mathf.Abs((result.transform.position.x) - (_Transform.position.x)) <= _Range)
                {
                    if (direction != 0)
                        oldDirection = direction;
                    direction = 0;
                }
                else
                {
                    direction = oldDirection;
                }

                if (result.transform.position.x < _Transform.position.x)
                {
                    if (direction == 1)
                    {
                        direction *= -1;
                        makeRotation = true;
                    }
                }
                else
                {
                    if (direction == -1)
                    {
                        direction *= -1;
                        makeRotation = true;
                    }
                }
                PlayerDetect = true;
            }
        }
        Debug.Log(PlayerDetect + " " + direction + " " + oldDirection);
        if (!PlayerDetect && direction == 0)
        {
            direction = oldDirection;
        }
        if (makeRotation)
        {
            //_Transform.rotation *= Quaternion.Euler(0, 180, 0);

            makeRotation = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_Transform == null)
        {
            return;
        }
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
            Vector3 newVelocity = direction * _Transform.right * _TranslationSpeed;
            if (PlayerDetect)
            {
                newVelocity *= 2;
            }
            Vector3 velocityChange = newVelocity - _Rigidbody.velocity;
            _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}