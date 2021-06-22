using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionAttack : MonoBehaviour
{
    #region PrivateFields

    private EnemyMovement _Movement;

    private Transform _Transform;

    private Animator _Animator;

    #endregion PrivateFields

    #region PublicHideProperties

    [HideInInspector]
    public bool CanAttack;

    [HideInInspector]
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
        _Animator = GetComponentInChildren<Animator>();
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

    /// <summary>
    ///     Execute the attack animantion of an ennemy, with is sound attack
    /// </summary>
    ///
    /// <returns>
    ///     A boolean if object is detect
    /// </returns>
    public void AttackAnimation()
    {
        _Animator.SetBool("IsAttacking", true);

        StartCoroutine(
            Util.ExecuteAfterTime(0.5f, () =>
            {
                AudioManager.Instance.Play(_AttackSound);
                _Animator.SetBool("IsAttacking", false);
            })
        );
    }

    #endregion UnityMethods
}