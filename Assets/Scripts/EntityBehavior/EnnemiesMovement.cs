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

    [Header("Movement")]
    [SerializeField]
    private float _TranslationSpeed;

    [Header("RaycastDetection")]
    [SerializeField]
    private LayerMask _ObjectToDetect;

    [SerializeField]
    private List<MyRay> _DetectDirections;

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

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!makeRotation)
            {
                makeRotation = true;
                direction *= -1;
            }
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger Player");
        }
    }

    private bool PlayerDetect;

    private float oldDirection = 1;

    private void Update()
    {
        PlayerDetect = false;

        foreach (MyRay ray in _DetectDirections)
        {
            if (Physics.Raycast(new Ray(_Transform.position + ray.Origin, (ray.Destination)), out RaycastHit result, ray.Range, _ObjectToDetect))
            {
                Debug.Log("Collision");
                if (Mathf.Abs((result.transform.position.x) - (_Transform.position.x)) <= _Range)
                {
                    if (direction != 0)
                        oldDirection = direction;
                    direction = 0;
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

        if (!PlayerDetect && direction == 0)
        {
            direction = oldDirection;
        }
        if (makeRotation)
        {
            _Transform.GetChild(0).rotation *= Quaternion.Euler(0, 180, 0);

            makeRotation = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_Transform == null)
        {
            return;
        }
        foreach (MyRay ray in _DetectDirections)
        {
            Gizmos.color = Color.red;
            Vector3 direction = (ray.Destination) * ray.Range;

            Gizmos.DrawRay((_Transform.position + ray.Origin), direction);
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