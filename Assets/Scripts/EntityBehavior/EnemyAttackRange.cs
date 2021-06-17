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
        
        GameObject newBallGo = Instantiate(_BallPrefab);
        newBallGo.transform.position = _BallSpawnPosition.position;
        Transform TargetTransform = _DetectionAttack.Target.transform;
        newBallGo.GetComponent<Projectile>().Target = new Vector3(TargetTransform.position.x, TargetTransform.position.y - 0.5f, TargetTransform.position.z); 
        newBallGo.GetComponent<WeaponBehaviour>().Holder = gameObject.transform.parent.gameObject;

        Destroy(newBallGo, _BallLifeDuration);
    }

    #endregion UnityMethods
}