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
    [SerializeField]
    private bool isGrounded;
    private Vector3 velocityChange;
    private int jumpCollision;

    // Start is called before the first frame update
    void Start()
    {
        _Transform = GetComponent<Transform>();
        _Rigidbody = GetComponent<Rigidbody>();
        lastDirection = 1;
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            Vector3 newJumpVelocity = _Transform.up * _JumpSpeed * Input.GetAxis("Jump");
            _Rigidbody.AddForce(newJumpVelocity, ForceMode.Impulse);
        }

        float directionSign = Mathf.Sign(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") != 0 && directionSign != lastDirection)
        {
            _Transform.GetChild(0).rotation *= Quaternion.Euler(0, 180, 0);
            lastDirection = directionSign;
        }
        
        Vector3 newVelocity = _Transform.forward * _TranslationSpeed * Input.GetAxis("Horizontal");
        velocityChange = newVelocity - _Rigidbody.velocity;
        velocityChange.y = 0;
        if (jumpCollision == -1 && Mathf.Sign(velocityChange.x)==-1)
        {
            velocityChange.x = 0;
        }
        if (jumpCollision == 1 && Mathf.Sign(velocityChange.x) == 1)
        {
            velocityChange.x = 0;
        }
        _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        float direction = collision.transform.position.x - transform.position.x;
        if (collision.gameObject.transform.parent.CompareTag("Ground") && collision.gameObject.transform.position.y < _Transform.position.y )
        {
            isGrounded = true;
        }
        if(collision.gameObject.transform.parent.CompareTag("Wall") && !velocityChange.Equals(Vector3.zero))
        {
            //Debug.Log("enter");
            if (direction > 0)
                jumpCollision = 1;
            else
                jumpCollision = -1;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        float direction = collision.transform.position.x - transform.position.x;
        bool stay = false;
        if (collision.gameObject.transform.parent.CompareTag("Wall") && !velocityChange.Equals(Vector3.zero) && !isGrounded)
        {
            //Debug.Log("stay");
            stay = true;
            if (direction > 0)
                jumpCollision = 1;
            else
                jumpCollision = -1;
        }
        if(!isGrounded && stay == false) {
            if (jumpCollision == -1) { transform.position += new Vector3(0.1f,0,0); }
            if (jumpCollision == 1) { transform.position += new Vector3(-0.1f, 0, 0); }
            jumpCollision = 0;
            //Debug.Log("jumpCollision 0"); 
        }
        if (collision.gameObject.transform.parent.CompareTag("Ground") && collision.gameObject.transform.position.y < _Transform.position.y)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform.parent.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.transform.parent.CompareTag("Wall")) { 
            jumpCollision = 0; 
            //Debug.Log("exit"); 
        }
    }
}
