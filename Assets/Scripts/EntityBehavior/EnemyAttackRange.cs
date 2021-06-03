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

    public Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void Update()
    {
        if (_DetectionAttack.CanAttack && _BallNextShotTime < Time.time)
        {
            GameObject newBallGo = Instantiate(_BallPrefab);
            newBallGo.transform.position = _BallSpawnPosition.position;
            newBallGo.GetComponent<Projectile>().targetPos = target.position;

            //Vector3.Lerp(newBallGo.transform.position, target.transform.position, _BallStartVelocity * Time.deltaTime);

            newBallGo.GetComponent<Rigidbody>().velocity = _BallSpawnPosition.right * _BallStartVelocity;
            newBallGo.GetComponent<WeaponBehaviour>().Holder = gameObject;

            //Vector3.Slerp(gameObject.transform.forward, newBallGo.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime * 2);

            ////Debug.Log(newBallGo.transform.position + " " + _BallSpawnPosition.position);
            ///

            _BallNextShotTime = Time.time + _BallCoolDownDuration;

            Destroy(newBallGo, _BallLifeDuration);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 dir;
    //    dir = target.position - _BallSpawnPosition.position;
    //    float x = dir.magnitude;
    //    float y = -_BallSpawnPosition.position.y;
    //    dir /= x;

    //    float g = 9.81f;
    //    float s = 5f;
    //    float s2 = s * s;

    //    float r = s2 * s2 - g * (g * x * x + 2f * y * s2);

    //    Debug.Log((Mathf.Sqrt(r)) + " " + r + " " + g);
    //    float tanTheta = (s2 + Mathf.Sqrt(Mathf.Abs(r))) / (g * x);
    //    float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
    //    float sinTheta = cosTheta * tanTheta;
    //    Vector3 prev = _BallSpawnPosition.position, next;
    //    for (int i = 1; i <= 10; i++)
    //    {
    //        float t = i / 10f;
    //        float dx = s * cosTheta * t;
    //        float dy = s * sinTheta * t - 0.5f * g * t * t;
    //        next = _BallSpawnPosition.position + new Vector3(dir.x * dx, dy, dir.z);
    //        Debug.DrawLine(prev, next, Color.blue, 1f);
    //        prev = next;
    //    }
    //    Debug.DrawLine(_BallSpawnPosition.position, target.position, Color.yellow, 1f);
    //    Debug.DrawLine(
    //        new Vector3(_BallSpawnPosition.position.x, 0.01f, _BallSpawnPosition.position.z),
    //        new Vector3(
    //            _BallSpawnPosition.position.x + dir.x * x, 0.01f, _BallSpawnPosition.position.z + dir.y * x
    //        ),
    //        Color.white,
    //        1f
    //    );
    //}

    #endregion UnityMethods
}