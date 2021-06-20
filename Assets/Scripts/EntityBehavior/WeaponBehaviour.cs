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
            Debug.Log("Ennemy");
            if (other.gameObject.CompareTag("Player") && other.isTrigger == false && !other.gameObject.GetComponent<Player>().IsBlocking && nextAttackTime < Time.time)
            {
                var _EnemyDetectionAttack = GetComponentInParent<EnemyDetectionAttack>();
                var _EnemyAttackRange = GetComponentInParent<EnemyAttackRange>();
                if (_EnemyDetectionAttack  != null && _EnemyAttackRange == null)
                {
                    Debug.Log("Axeman" + nextAttackTime);
                    _EnemyDetectionAttack.AttackAnimation();
                    StartCoroutine(AnimationAxeMan(other.gameObject));
                }
                else
                {

                    nextAttackTime = Time.time + attackCoolDownDuration;
                    other.gameObject.GetComponent<LivingEntity>().Damage(1);
                }
             
            }
        }
    }

    private IEnumerator AnimationAxeMan(GameObject otherGameObject)
    {
     ;
        if(otherGameObject != null)
        {
            nextAttackTime = Time.time + attackCoolDownDuration;
            yield return new WaitForSeconds(.5f);
            otherGameObject.GetComponent<LivingEntity>().Damage(1);
            Debug.Log("Axeman toto"+ attackCoolDownDuration);
            
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