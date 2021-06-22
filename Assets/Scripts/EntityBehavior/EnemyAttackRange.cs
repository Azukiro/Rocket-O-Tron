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

    [Header("Spear")]
    [SerializeField]
    private GameObject _SpearPrefab;

    [SerializeField]
    private float _SpearStartVelocity;

    [SerializeField]
    private float _SpearLifeDuration;

    [SerializeField]
    private float _SpearCoolDownDuration;

    [SerializeField]
    private Transform _SpearSpawnPosition;

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
            _BallNextShotTime = Time.time + _SpearCoolDownDuration;
            _DetectionAttack.AttackAnimation();
        }
    }

    public void LaunchSpear()
    {
        GameObject newBallGo = Instantiate(_SpearPrefab);
        newBallGo.transform.position = _SpearSpawnPosition.position;
        Transform TargetTransform = _DetectionAttack.Target.transform;
        newBallGo.GetComponent<Projectile>().Target = new Vector3(TargetTransform.position.x, TargetTransform.position.y - 0.5f, TargetTransform.position.z);
        newBallGo.GetComponent<WeaponBehaviour>().Holder = gameObject.transform.parent.gameObject;

        Destroy(newBallGo, _SpearLifeDuration);
    }

    #endregion UnityMethods
}