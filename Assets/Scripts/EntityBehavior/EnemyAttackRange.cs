using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    #region PrivateFields

    private EnemyDetectionAttack _DetectionAttack;

    private float _BallNextShotTime;

    #endregion PrivateFields

    #region PrivateSerializeFields
    [Header("Ball")]
    [SerializeField]
    private GameObject _BallPrefab;

    [SerializeField]
    private float _BallStartVelocity;

    [SerializeField]
    private float _BallLifeDuration;

    [SerializeField]
    private float _BallCoolDownDuration;

    [SerializeField]
    private Transform _BallSpawnPosition;


    #endregion PrivateSerializeFields

    #region UnityMethods

    private void Awake()
    {
        _DetectionAttack = GetComponentInParent<EnemyDetectionAttack>();
    }




    public bool IsAttacking;

    private void Update()
    {
        if (_DetectionAttack.CanAttack && _BallNextShotTime < Time.time)
        {
            _BallNextShotTime = Time.time + _BallCoolDownDuration;
            _DetectionAttack.AttackAnimation();
           
        }
    }

    public void LaunchSpear()
    {
        Debug.Log("LauchSpear");
        GameObject newBallGo = Instantiate(_BallPrefab);
        newBallGo.transform.position = _BallSpawnPosition.position;
        newBallGo.GetComponent<Projectile>().Target = _DetectionAttack.Target.transform;
        newBallGo.GetComponent<WeaponBehaviour>().Holder = gameObject;

        Destroy(newBallGo, _BallLifeDuration);
    }

    #endregion UnityMethods
}