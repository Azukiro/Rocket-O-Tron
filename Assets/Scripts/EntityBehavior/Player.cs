using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /**
     * Unity variables
    **/

    [Header("Movement")]
    [SerializeField] private float _TranslationSpeed;

    [SerializeField] private float _JumpSpeed;
    [SerializeField] private bool isGrounded;

    /**
     * Component variables
    **/
    private Rigidbody _Rigidbody;
    private Transform _Transform;
    private Animator _animator;

    /**
     * Local variables
    **/
    private float lastDirection;
    private Vector3 velocityChange;
    private int jumpCollision;
    private bool isJumping;

    // Start is called before the first frame update
    private void Start()
    {
        // Store the component variables
        _Transform = GetComponent<Transform>();
        _Rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();

        // Init local variables
        lastDirection = 1;
    }

    private void FixedUpdate()
    {
        // Store user inputs
        float jumpInput = Input.GetAxis("Jump");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the Player
        Vector3 newVelocity = _Transform.forward * _TranslationSpeed * horizontalInput;
        velocityChange = newVelocity - _Rigidbody.velocity;
        velocityChange.y = 0;

        // Rotate the Player
        float directionSign = Mathf.Sign(horizontalInput);
        if (horizontalInput != 0 && directionSign != lastDirection)
        {
            _Transform.GetChild(0).rotation *= Quaternion.Euler(0, 180, 0);
            lastDirection = directionSign;
        }

        // Jump the Player
        if (isGrounded && jumpInput != 0)
        {
            Vector3 newJumpVelocity = _Transform.up * _JumpSpeed * jumpInput;
            _Rigidbody.AddForce(newJumpVelocity, ForceMode.Impulse);
        }

        // Jump collisions
        if ((jumpCollision == -1 && Mathf.Sign(velocityChange.x) == -1) || (jumpCollision == 1 && Mathf.Sign(velocityChange.x) == 1))
        {
            velocityChange.x = 0;
        }

        // Add movement forces
        _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void Update()
    {
        // Animations
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        // Store user inputs
        float jumpInput = Input.GetAxis("Jump");
        float fire1Input = Input.GetAxis("Fire1");
        float fire2Input = Input.GetAxis("Fire2");
        float fire3Input = Input.GetAxis("Fire3");

        //  - Update Speed to raise left / right animations
        float xSpeed = Mathf.Abs(_Rigidbody.velocity.x);
        _animator.SetFloat("Speed", xSpeed);

        //  - Update IsJumping to raise / stop a jump animation
        if (isGrounded)
        {
            // End of a jump
            if (isJumping)
            {
                isJumping = false;
                _animator.SetBool("IsJumping", isJumping);
            }

            // Start of a jump
            if (jumpInput != 0)
            {
                isJumping = true;
                _animator.SetBool("IsJumping", isJumping);
            }
        }

        //  - Update IsAttacking to raise an attack animation
        if (fire1Input != 0)
        {
            _animator.SetBool("IsAttacking", true);
            ExecuteAfterTime(0.1f, () => _animator.SetBool("IsAttacking", false));
        }

        //  - Update IsAttackingBig to raise a big attack animation
        if (fire2Input != 0)
        {
            _animator.SetBool("IsAttackingBig", true);
            ExecuteAfterTime(0.1f, () => _animator.SetBool("IsAttackingBig", false));
        }

        //  - Update IsBlocking to raise a defense animation
        if (fire3Input != 0)
        {
            _animator.SetBool("IsBlocking", true);
            ExecuteAfterTime(0.1f, () => _animator.SetBool("IsBlocking", false));
        }
    }

    private delegate void VoidCallback();

    // Execute some code after time
    private void ExecuteAfterTime(float time, VoidCallback callback)
    {
        StartCoroutine(_ExecuteAfterTime(time, callback));
    }

    private IEnumerator _ExecuteAfterTime(float time, VoidCallback callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float direction = collision.transform.position.x - transform.position.x;
        if (collision.gameObject.transform.parent.CompareTag("Ground") && collision.gameObject.transform.position.y < _Transform.position.y)
        {
            isGrounded = true;
        }
        if (collision.gameObject.transform.parent.CompareTag("Wall") && !velocityChange.Equals(Vector3.zero))
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
        if (!isGrounded && stay == false)
        {
            if (jumpCollision == -1) { transform.position += new Vector3(0.1f, 0, 0); }
            if (jumpCollision == 1) { transform.position += new Vector3(-0.1f, 0, 0); }
            jumpCollision = 0;
            //Debug.Log("jumpCollision 0");
        }
        if (collision.gameObject.transform.parent.CompareTag("Ground") && collision.gameObject.transform.position.y < _Transform.position.y)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform.parent.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.transform.parent.CompareTag("Wall"))
        {
            jumpCollision = 0;
            //Debug.Log("exit");
        }
    }
}