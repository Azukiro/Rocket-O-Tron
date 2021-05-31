using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesDetectionAttack : MonoBehaviour
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

    public bool CanAttack;

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
            if (Mathf.Abs((other.gameObject.transform.position.x) - (_Transform.position.x)) <= _Range)
            {
                CanAttack = true;
                if (_Movement.Direction != 0)
                    _Movement.OldDirection = _Movement.Direction;
                _Movement.Direction = 0;
            }
            else
            {
                CanAttack = false;
            }

            if (other.gameObject.transform.position.x < _Transform.position.x)
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

            if (!_Movement.PlayerDetect && _Movement.Direction == 0)
            {
                _Movement.Direction = _Movement.OldDirection;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _Movement.PlayerDetect = false;
            CanAttack = false;
        }
    }
}