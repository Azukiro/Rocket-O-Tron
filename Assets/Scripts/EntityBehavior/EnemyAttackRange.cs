using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    #region PrivateFields

    private EnemyDetectionAttack _DetectionAttack;

    private float _SpearNextShotTime;

    #endregion PrivateFields

    #region PublicHideProperties

    [HideInInspector]
    public bool IsAttacking;

    #endregion PublicHideProperties

    #region PrivateSerializeFields

    [Header("Spear")]
    [SerializeField]
    private GameObject _SpearPrefab;

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

    private void Update()
    {
        if (_DetectionAttack.CanAttack && _SpearNextShotTime < Time.time)
        {
            _SpearNextShotTime = Time.time + _SpearCoolDownDuration;
            _DetectionAttack.AttackAnimation();
        }
    }

    /// <summary>
    ///     Throw the spear with instantiate a spear prefab
    /// </summary>
    public void LaunchSpear()
    {
        GameObject newSpear = Instantiate(_SpearPrefab);

        newSpear.transform.position = _SpearSpawnPosition.position;
        Transform TargetTransform = _DetectionAttack.Target.transform;

        newSpear.GetComponent<Projectile>().Target = new Vector3(TargetTransform.position.x, TargetTransform.position.y - 0.5f, TargetTransform.position.z);
        newSpear.GetComponent<WeaponBehaviour>().Holder = gameObject.transform.parent.gameObject;

        Destroy(newSpear, _SpearLifeDuration);
    }

    #endregion UnityMethods
}