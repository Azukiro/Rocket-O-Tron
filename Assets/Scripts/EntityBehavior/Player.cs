using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody _Rigidbody;

    private Transform _Transform;

    private float lastDirection;

    [Header("Movement")]
    [SerializeField]
    private float _TranslationSpeed;
    [SerializeField]
    private float _JumpSpeed;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _Transform = GetComponent<Transform>();
        _Rigidbody = GetComponent<Rigidbody>();
        lastDirection = 1;
    }

    private void FixedUpdate()
    {
        float directionSign = Mathf.Sign(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") != 0 && directionSign != lastDirection)
        {
            _Transform.GetChild(0).rotation *= Quaternion.Euler(0, 180, 0);
            lastDirection = directionSign;
        }

        Vector3 newVelocity = _Transform.forward * _TranslationSpeed * Input.GetAxis("Horizontal");
        Vector3 velocityChange = newVelocity - _Rigidbody.velocity;
        velocityChange.y = 0;
        _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        if (isGrounded)
        {
            Vector3 newJumpVelocity = _Transform.up * _JumpSpeed * Input.GetAxis("Jump");
            _Rigidbody.AddForce(newJumpVelocity, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && collision.gameObject.transform.position.y < _Transform.position.y )
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrounded = false;
        }
    }
}
