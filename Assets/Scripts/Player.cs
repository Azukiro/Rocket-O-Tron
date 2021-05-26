using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Motion Setup")]
    [Tooltip("unit : m.s-1")]
    [SerializeField] float m_translationSpeed; // m/s
    [Tooltip("unit : °.s-1")]
    [SerializeField] float m_rotationSpeed; // °/s

    Transform m_Transform;
    Rigidbody m_Rigidbody;

    private void Awake()
    {
        m_Transform = transform;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 2 types de comportements : 
    // comportement cinématique -> Update() ... force les position/orientation de l'objet via script

    // Update is called once per frame
    void Update()
    {
        /*
        float vInput = Input.GetAxis("Vertical"); // entre -1 et 1
        float hInput = Input.GetAxis("Horizontal"); // entre -1 et 1

        //Translation
        Vector3 moveVect = vInput * m_Transform.forward * m_translationSpeed * Time.deltaTime;
        //m_Transform.position += moveVect;
        m_Transform.Translate(moveVect,Space.World);

        //Rotation
        float angle = hInput * m_rotationSpeed * Time.deltaTime;
        m_transform.Rotate(m_transform.up,angle,Space.World);
        */
    }

    // comportement cinétique -> FixedUpdate() ... les position/orientation de l'objet sont calculées par le moteur physique
    // rigidbody, Time.fixedDeltaTime
    private void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical"); // entre -1 et 1
        float hInput = Input.GetAxisRaw("Horizontal"); // entre -1 et 1

        //MovePosition & MoveRotation
        /*//Translation
        Vector3 moveVect = vInput * m_Transform.forward * m_translationSpeed * Time.fixedDeltaTime;
        Vector3 newPos = m_Rigidbody.position + moveVect;
        m_Rigidbody.MovePosition(newPos);

        //Rotation
        float angle = hInput * m_rotationSpeed * Time.fixedDeltaTime;
        Quaternion qRot = Quaternion.AngleAxis(angle, m_Transform.up);
        //Quaternion de redressement
        Quaternion qUprightRot = Quaternion.FromToRotation(m_Transform.up, Vector3.up);

        //interpolations linéaires et sphériques
        Quaternion qSlightUprightOrientation = Quaternion.Slerp(m_Rigidbody.rotation, qUprightRot * m_Rigidbody.rotation, Time.fixedDeltaTime * 4);

        Quaternion newOrientation = qUprightRot * qRot * m_Rigidbody.rotation;  
        m_Rigidbody.MoveRotation(newOrientation);*/

        //AddForce & AddTorque
        Vector3 newVelocity = m_Transform.forward * m_translationSpeed*vInput;
        Vector3 velocityChange = newVelocity - m_Rigidbody.velocity;
        m_Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        Vector3 newAngularVelocity = m_Transform.up * m_rotationSpeed *Mathf.Deg2Rad*hInput;
        Vector3 angularVelocityChange = newAngularVelocity - m_Rigidbody.angularVelocity;
        m_Rigidbody.AddTorque(angularVelocityChange, ForceMode.VelocityChange);

        //Debug
        //Debug.DrawLine(m_Transform.position + Vector3.up, m_Transform.position + Vector3.up + m_Rigidbody.velocity,Color.red);
        //if (Time.time > 3) Debug.Break();
    }

    //private void OnDrawGizmos()
    //{
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(m_Transform.position + Vector3.up, m_Transform.position + Vector3.up + m_Rigidbody.velocity);
    //}
}
