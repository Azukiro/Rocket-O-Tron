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
            //If Player in range prepare to attack
            if (Mathf.Abs((other.gameObject.transform.position.x) - (_Transform.position.x)) <= _Range)
            {
                CanAttack = true;
                _Movement.Freeze = true;
            }
            else
            {
                //Reset CanAttack and enabled movement
                CanAttack = false;
                if (_Movement.Freeze)
                {
                    _Movement.Freeze = false; ;
                }
            }

            //If Player left and back of ennemy rotate ennemy
            if (other.gameObject.transform.position.x < _Transform.position.x)
            {
                if (_Movement.Direction == 1)
                {
                    Debug.Log("Left");
                    _Movement.Direction *= -1;
                    _Movement.MakeRotation = true;
                }
            }
            else
            {
                //If Player right and back of ennemy rotate ennemy
                if (_Movement.Direction == -1)
                {
                    Debug.Log("Right");
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