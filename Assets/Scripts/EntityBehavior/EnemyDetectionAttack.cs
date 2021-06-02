using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionAttack : MonoBehaviour
{
    #region PrivateFields

    private EnemyMovement _Movement;

    private Transform _Transform;

    #endregion PrivateFields

    #region PublicHideProperties

    [HideInInspector]
    public bool CanAttack;

    #endregion PublicHideProperties

    #region PrivateSerializeFields

    [SerializeField]
    private int _Range;

    #endregion PrivateSerializeFields

    #region UnityMethods

    private void Awake()
    {
        _Movement = GetComponent<EnemyMovement>();
        _Transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player" + _Movement.Direction + " " + _Movement.OldDirection);

            //If Player in range prepare to attack
            if (Mathf.Abs((other.gameObject.transform.position.x) - (_Transform.position.x)) <= _Range)
            {
                CanAttack = true;
                if (_Movement.Direction != 0)
                    _Movement.OldDirection = _Movement.Direction;
                _Movement.Direction = 0;
            }
            else
            {
                //Reset CanAttack and enabled movement
                CanAttack = false;
                if (_Movement.Direction == 0)
                {
                    _Movement.Direction = _Movement.OldDirection;
                }
            }

            //If Player left and back of ennemy rotate ennemy
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
                //If Player right and back of ennemy rotate ennemy
                if (_Movement.Direction == -1)
                {
                    _Movement.Direction *= -1;
                    _Movement.MakeRotation = true;
                }
            }
            _Movement.PlayerDetect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))//Reset playerDetectionField
        {
            _Movement.PlayerDetect = false;
            CanAttack = false;
        }
    }

    #endregion UnityMethods
}