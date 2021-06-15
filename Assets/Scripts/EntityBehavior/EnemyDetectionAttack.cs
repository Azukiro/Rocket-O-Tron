using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionAttack : MonoBehaviour
{
    #region PrivateFields

    private EnemyMovement _Movement;

    private Transform _Transform;

    private Animator _animator;

    #endregion PrivateFields

    #region PublicHideProperties

    [HideInInspector]
    public bool CanAttack;
    public GameObject Target;

    #endregion PublicHideProperties

    #region PrivateSerializeFields

    [SerializeField]
    private int _Range;

    [SerializeField]
    private string _AttackSound;

    #endregion PrivateSerializeFields

    #region UnityMethods

    private void Awake()
    {
        _Movement = GetComponent<EnemyMovement>();
        _Transform = GetComponent<Transform>();
        _animator = GetComponentInChildren<Animator>();
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
                Target = other.gameObject;
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

    public void AttackAnimation()
    {
        _animator.SetBool("IsAttacking", true);

        StartCoroutine(
            Util.ExecuteAfterTime(0.5f, () =>
            {
                AudioManager.Instance.Play(_AttackSound);
                _animator.SetBool("IsAttacking", false);
            })
        );
    }

    #endregion UnityMethods
}