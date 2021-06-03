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

public class EnemyMovement : MonoBehaviour
{
    #region PrivateFields

    private Rigidbody _Rigidbody;

    private Transform _Transform;

    #endregion PrivateFields

    #region PublicHideProperties

    [HideInInspector]
    public bool MakeRotation;

    [HideInInspector]
    public float Direction = 1;

    [HideInInspector]
    public bool Freeze = false;

    [HideInInspector]
    public bool PlayerDetect;

    #endregion PublicHideProperties

    #region PrivateSerializeFields

    [Header("Movement")]
    [SerializeField]
    private float _TranslationSpeed;

    [SerializeField]
    private float _Acceleration;

    #endregion PrivateSerializeFields

    #region UnityMethods

    private void Awake()
    {
        _Transform = GetComponent<Transform>();
        _Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (MakeRotation)
        {
            _Transform.GetChild(0).rotation *= Quaternion.Euler(0, 180, 0);//Rotate Gfx
            MakeRotation = false;
        }
    }

    private void FixedUpdate()
    {
        if (!MakeRotation && !Freeze)
        {
            Vector3 newVelocity = Direction * _Transform.right * _TranslationSpeed;
            if (PlayerDetect)//Accelerate is player detected
            {
                newVelocity *= _Acceleration;
            }

            Vector3 velocityChange = newVelocity - _Rigidbody.velocity;
            velocityChange.y = 0;

            _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    #endregion UnityMethods
}