using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{

    private Transform _Transform;


    [Header("Detection")]
    private bool PlayerDetect;
    [SerializeField]
    private LayerMask _ObjectToDetect;
    [SerializeField]
    private List<Vector3> _DetectDirections;
    [SerializeField]
    private int _MaxDetectionDistance;

    private void Awake()
    {
        _Transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
