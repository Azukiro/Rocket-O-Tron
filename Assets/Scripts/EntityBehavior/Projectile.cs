using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class Projectile : MonoBehaviour
{
    #region PrivateFields

    public Vector3 Target;

    public float firingAngle = 45.0f;

    public float gravity = 9.8f;

    private Transform myTransform;

    #endregion PrivateFields

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        StartCoroutine(SimulateProjectile());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///     This is a coroutine for launch the spear with a parabolic movement Source of algotihm : <see href="https://gist.github.com/jackchen1210/61fa983c3089dc4b6d58ff44ec47c540">here</see>
    /// </summary>
    private IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        if (Target == null)
        {
            yield return null;
        }

        // Move projectile to the position of throwing object + add some offset if needed.
        myTransform.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(myTransform.position, Target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        if (Target.x < myTransform.position.x)
            myTransform.rotation *= Quaternion.Euler(0, 180, 0);//Rotate Gfx

        float elapse_time = 0;

        while (true)
        {
            myTransform.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}