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
        _DetectionAttack = GetComponent<EnemyDetectionAttack>();
    }

    private void Update()
    {
        if (_DetectionAttack.CanAttack && _BallNextShotTime < Time.time)
        {
            GameObject newBallGo = Instantiate(_BallPrefab);
            newBallGo.transform.position = _BallSpawnPosition.position;
            newBallGo.GetComponent<Rigidbody>().velocity = _BallSpawnPosition.right * _BallStartVelocity;
            newBallGo.GetComponent<WeaponBehaviour>().Holder = gameObject;
            Vector3.Slerp(gameObject.transform.forward, newBallGo.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime * 2);

            //Debug.Log(newBallGo.transform.position + " " + _BallSpawnPosition.position);
            _BallNextShotTime = Time.time + _BallCoolDownDuration;

            Destroy(newBallGo, _BallLifeDuration);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(_BallSpawnPosition.position, _BallSpawnPosition.right * _BallStartVelocity);
    }

    #endregion UnityMethods
}