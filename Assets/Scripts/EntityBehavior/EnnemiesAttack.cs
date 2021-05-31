using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesAttack : MonoBehaviour
{
    private EnnemiesDetectionAttack _DetectionAttack;

    [Header("Ball")]
    [SerializeField]
    private GameObject _BallPrefab;

    [SerializeField]
    private float _BallStartVelocity;

    [SerializeField]
    private float _BallLifeDuration;

    [SerializeField]
    private float _BallCoolDownDuration;

    private float _BallNextShotTime;

    [SerializeField]
    private Transform _BallSpawnPosition;

    private void Awake()
    {
        _DetectionAttack = GetComponent<EnnemiesDetectionAttack>();
    }

    private void Update()
    {
        if (_DetectionAttack.CanAttack && _BallNextShotTime < Time.time)
        {
            GameObject newBallGo = Instantiate(_BallPrefab);
            newBallGo.transform.position = _BallSpawnPosition.position;
            newBallGo.GetComponent<Rigidbody>().velocity = _BallSpawnPosition.forward * _BallStartVelocity;
            _BallNextShotTime = Time.time + _BallCoolDownDuration;
            Destroy(newBallGo, _BallLifeDuration);
        }
    }
}