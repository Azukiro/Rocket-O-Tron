using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject Holder;

    private void OnTriggerEnter(Collider other)
    {
        if (Holder == null)
        {
            return;
        }

        //IF PLAYER ATTACKS
        if (Holder.CompareTag("Player") && Holder.GetComponent<Player>().CanAttack)
        {
            //Debug.Log("triggerenter");
            /*
            if (other.gameObject.CompareTag("Weapon") && holder.CompareTag("Player"))
            {
                Destroy(other.gameObject);
                Vector3 firstExplosionPoint = other.gameObject.GetComponent<WeaponBehaviour>().holder.transform.position;
                firstExplosionPoint.y += 1;
                holder.GetComponent<Rigidbody>().AddExplosionForce(5000, firstExplosionPoint, 5);

                GameObject secondHolder = other.gameObject.GetComponent<WeaponBehaviour>().holder;
                Vector3 secondExplosionPoint = holder.transform.position;
                secondExplosionPoint.y += 1;
                secondHolder.GetComponent<Rigidbody>().AddExplosionForce(1000, secondExplosionPoint, 5);
            }*/

            //If player touch enemy
            if (other.gameObject.CompareTag("Enemy") && other.isTrigger == false)
            {
                other.gameObject.GetComponent<LivingEntity>().Damage(1);
                Debug.DrawLine(other.transform.position, -other.transform.GetChild(0).forward * 100, Color.red);
                other.GetComponent<Rigidbody>().AddForce(-other.transform.forward * 100, ForceMode.Impulse);
            }
        }

        //IF ENEMY ATTACKS
        if (Holder.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("Player") && other.isTrigger == false && !other.gameObject.GetComponent<Player>().IsBlocking)
            {
                other.gameObject.GetComponent<LivingEntity>().Damage(1);
            }
        }
    }
}