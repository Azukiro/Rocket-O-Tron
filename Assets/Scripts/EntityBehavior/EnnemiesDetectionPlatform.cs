using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesDetectionPlatform : MonoBehaviour
{
    #region PrivateFields

    private EnnemiesMovement _Movement;

    private Transform _Transform;

    private Collider _Collider;

    #endregion PrivateFields

    #region PublicHideProperties

    [HideInInspector]
    public bool isGrounded = false;

    #endregion PublicHideProperties

    #region PrivateSerializeFields

    [SerializeField]
    [Min(1f)]
    private float _RaycastRange = 1f;

    #endregion PrivateSerializeFields

    #region UnityMethods

    private void Awake()
    {
        _Movement = GetComponent<EnnemiesMovement>();
        _Transform = GetComponent<Transform>();
        _Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        //Down Raycast for ground
        Vector3 downPosition = new Vector3(_Transform.position.x + (.1f + _Collider.bounds.size.x) * _Movement.Direction, _Transform.position.y, _Transform.position.z);
        if (!CheckRaycast(downPosition, Vector3.down, _RaycastRange, LayerMask.GetMask("Ground")))
        {
            if (isGrounded)
            {
                _Movement.Direction *= -1;
                _Movement.MakeRotation = true;
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = true;
        }

        //wall detection right
        if (CheckRaycast(_Transform.position, Vector3.right, _RaycastRange, LayerMask.GetMask("Wall")))
        {
            if (_Movement.Direction == 1)
            {
                _Movement.Direction *= -1;

                _Movement.MakeRotation = true;
            }
        }

        //wall detection left
        if (CheckRaycast(_Transform.position, Vector3.left, _RaycastRange, LayerMask.GetMask("Wall")))
        {
            if (_Movement.Direction == -1)
            {
                _Movement.Direction *= -1;
                _Movement.MakeRotation = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_Collider == null || _Transform == null)
        {
            return;
        }
        Vector3 downPosition = new Vector3(_Transform.position.x + (.1f + _Collider.bounds.size.x) * _Movement.Direction, _Transform.position.y, _Transform.position.z);
        Vector3 direction = Vector3.down * 1f;

        Gizmos.color = Color.red;

        Gizmos.DrawRay(downPosition, direction);

        Gizmos.DrawRay(_Transform.position, Vector3.left * 1f);
        Gizmos.DrawRay(_Transform.position, Vector3.right * 1f);
    }

    #endregion UnityMethods

    #region PrivateMethods

    private bool CheckRaycast(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layerMask)
    {
        return Physics.Raycast(new Ray(origin, direction), maxDistance, layerMask);
    }

    #endregion PrivateMethods
}