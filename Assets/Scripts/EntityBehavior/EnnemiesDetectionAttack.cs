using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesDetectionAttck : MonoBehaviour
{
    // Start is called before the first frame update
    private EnnemiesMovement _Movement;

    private Transform _Transform;

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
        _Movement = GetComponent<EnnemiesMovement>();
        _Transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger Player");
        }
    }

    private void Update()
    {
        _Movement.PlayerDetect = false;

        foreach (MyRay ray in _DetectDirections)
        {
            if (Physics.Raycast(new Ray(_Transform.position + ray.Origin, (ray.Destination)), out RaycastHit result, ray.Range, _ObjectToDetect))
            {
                Debug.Log("Collision");
                if (Mathf.Abs((result.transform.position.x) - (_Transform.position.x)) <= _Range)
                {
                    if (_Movement.Direction != 0)
                        _Movement.OldDirection = _Movement.Direction;
                    _Movement.Direction = 0;
                }

                if (result.transform.position.x < _Transform.position.x)
                {
                    if (_Movement.Direction == 1)
                    {
                        _Movement.Direction *= -1;

                        _Movement.MakeRotation = true;
                    }
                }
                else
                {
                    if (_Movement.Direction == -1)
                    {
                        _Movement.Direction *= -1;
                        _Movement.MakeRotation = true;
                    }
                }
                _Movement.PlayerDetect = true;
            }
        }

        if (!_Movement.PlayerDetect && _Movement.Direction == 0)
        {
            _Movement.Direction = _Movement.OldDirection;
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
}