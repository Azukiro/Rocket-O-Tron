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

    [SerializeField] private GameObject weapon;

    /**
     * States variables
    **/

    private Dictionary<string, bool> states = new Dictionary<string, bool>();

    public bool IsGrounded { get => states["IsGrounded"]; private set => states["IsGrounded"] = value; }

    public bool IsAttacking { get => states["IsAttacking"]; set => states["IsAttacking"] = value; }

    public bool IsAttackingBig { get => states["IsAttackingBig"]; set => states["IsAttackingBig"] = value; }

    public bool IsBlocking { get => states["IsBlocking"]; private set => states["IsBlocking"] = value; }

    public bool IsJumping { get => states["IsJumping"]; private set => states["IsJumping"] = value; }

    /**
     * Component variables
    **/

    private Rigidbody _Rigidbody;

    private Transform _Transform;

    private Animator _animator;

    private WeaponBehaviour weaponBehaviour;

    private LivingEntity livingEntity;

    /**
     * Local variables
    **/

    private float lastDirection;

    private Vector3 velocityChange;

    private int jumpCollision;

    private bool onJumpChange = false;

    // Start is called before the first frame update
    private void Start()
    {
        // Store the component variables
        _Transform = GetComponent<Transform>();
        _Rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        weaponBehaviour = weapon.GetComponent<WeaponBehaviour>();
        livingEntity = GetComponent<LivingEntity>();

        // Init local variables
        lastDirection = 1;
        IsGrounded = false;
        IsBlocking = false;
        IsJumping = false;
        IsAttacking = false;
        IsAttackingBig = false;
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
        if (IsGrounded)
        {
            if (jumpInput != 0)
            {
                // Start of a jump
                IsJumping = true;
                onJumpChange = true;

                // Add Jump Force
                Jump(jumpInput);
            }
            else if (IsJumping)
            {
                // End of a jump
                IsJumping = false;
                onJumpChange = true;
            }
        }

        // Jump collisions
        if ((jumpCollision == -1 && Mathf.Sign(velocityChange.x) == -1) || (jumpCollision == 1 && Mathf.Sign(velocityChange.x) == 1))
        {
            velocityChange.x = 0;
        }

        // Add movement forces
        _Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void Jump(float jumpInput)
    {
        Vector3 newJumpVelocity = _Transform.up * _JumpSpeed * jumpInput;
        _Rigidbody.AddForce(newJumpVelocity, ForceMode.Impulse);
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
        if (onJumpChange)
        {
            if (IsJumping)
            {
                // Start of a jump
                _animator.SetBool("IsJumping", IsJumping);
                AudioManager.Instance.Play("User jump");
            }
            else
            {
                // End of a jump
                _animator.SetBool("IsJumping", IsJumping);
            }
            onJumpChange = false;
        }

        //  - Update IsAttacking to raise an attack animation
        if (fire1Input != 0 && weaponBehaviour.IsAttackReset())
            SetAndUnsetAfterMillis("IsAttacking", "User attack");

        //  - Update IsAttackingBig to raise a big attack animation
        if (fire2Input != 0 && weaponBehaviour.IsBigAttackReset())
            SetAndUnsetAfterMillis("IsAttackingBig", "User big attack");

        //  - Update IsBlocking to raise a defense animation
        if (fire3Input != 0)
            SetAndUnsetAfterMillis("IsBlocking", "User block");
    }

    private void SetAndUnsetAfterMillis(string animationName, string soundName)
    {
        states[animationName] = true;
        _animator.SetBool(animationName, true);

        StartCoroutine(
            Util.ExecuteAfterTime(0.5f, () =>
            {
                AudioManager.Instance.Play(soundName);
                _animator.SetBool(animationName, false);
                states[animationName] = false;
            })
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent != null)
        {
            float direction = collision.transform.position.x - transform.position.x;
            if (collision.gameObject.transform.parent.CompareTag("Ground") && collision.gameObject.transform.position.y < _Transform.position.y)
            {
                IsGrounded = true;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Jump(1f);
            livingEntity.Damage(1f);
        }   
    }

    private void OnCollisionStay(Collision collision)
    {
        float direction = collision.transform.position.x - transform.position.x;
        bool stay = false;
        if (collision.gameObject.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.CompareTag("Wall") && !velocityChange.Equals(Vector3.zero) && !IsGrounded)
            {
                //Debug.Log("stay");
                stay = true;
                if (direction > 0)
                    jumpCollision = 1;
                else
                    jumpCollision = -1;
            }
            if (!IsGrounded && stay == false)
            {
                if (jumpCollision == -1) { transform.position += new Vector3(0.1f, 0, 0); }
                if (jumpCollision == 1) { transform.position += new Vector3(-0.1f, 0, 0); }
                jumpCollision = 0;

                //Debug.Log("jumpCollision 0");
            }
            if (collision.gameObject.transform.parent.CompareTag("Ground") && collision.gameObject.transform.position.y < _Transform.position.y)
            {
                IsGrounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.CompareTag("Ground"))
            {
                IsGrounded = false;
            }
            if (collision.gameObject.transform.parent.CompareTag("Wall"))
            {
                jumpCollision = 0;
            }
        }
    }
}