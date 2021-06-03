using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public Transform Target;

    public float firingAngle = 45.0f;

    public float gravity = 9.8f;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = base.transform;
    }

    private void Start()
    {
        StartCoroutine(SimulateProjectile());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("StopCorouTrigger");
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("StopCorouColision");
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown

        // Move projectile to the position of throwing object + add some offset if needed.
        transform.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        if (Target.position.x < transform.position.x)
            transform.rotation *= Quaternion.Euler(0, 180, 0);//Rotate Gfx

        float elapse_time = 0;

        while (true)
        {
            transform.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}