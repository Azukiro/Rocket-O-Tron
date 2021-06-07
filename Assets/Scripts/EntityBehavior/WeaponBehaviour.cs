using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject Holder;
    private float nextAttackTime;
    [SerializeField]
    private float attackCoolDownDuration;
    private float nextBigAttackTime;
    [SerializeField]
    private float bigAttackCoolDownDuration;

    private void OnTriggerEnter(Collider other)
    {/*
        if (Holder == null)
        {
            return;
        }

        //IF ENEMY ATTACKS
        if (Holder.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("Player") && other.isTrigger == false && !other.gameObject.GetComponent<Player>().IsBlocking)
            {
                other.gameObject.GetComponent<LivingEntity>().Damage(1);
            }
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (Holder == null)
        {
            return;
        }
        //IF PLAYER ATTACKS
        if (Holder.CompareTag("Player") && Holder.GetComponent<Player>().IsAttacking)
        {
            //If player touch enemy
            if (other.gameObject.CompareTag("Enemy") && other.isTrigger == false && nextAttackTime < Time.time)
            {
                other.gameObject.GetComponent<LivingEntity>().Damage(1);
                nextAttackTime = Time.time + attackCoolDownDuration;
            }
        }

        //IF PLAYER BIG ATTACKS
        if (Holder.CompareTag("Player") && Holder.GetComponent<Player>().IsAttackingBig)
        {
            //If player touch enemy
            if (other.gameObject.CompareTag("Enemy") && other.isTrigger == false && nextBigAttackTime < Time.time)
            {
                other.gameObject.GetComponent<LivingEntity>().Damage(2);
                nextBigAttackTime = Time.time + bigAttackCoolDownDuration;
            }
        }

        //IF ENEMY ATTACKS 
        if (Holder.CompareTag("Enemy"))
        {
            if (other.gameObject.CompareTag("Player") && other.isTrigger == false && !other.gameObject.GetComponent<Player>().IsBlocking && nextAttackTime < Time.time)
            {
                other.gameObject.GetComponent<LivingEntity>().Damage(1);
                nextAttackTime = Time.time + attackCoolDownDuration;
            }
        }
    }

    public bool IsBigAttackReset()
    {
        return nextBigAttackTime < Time.time;
    }

    public bool IsAttackReset()
    {
        return nextAttackTime < Time.time;
    }
}