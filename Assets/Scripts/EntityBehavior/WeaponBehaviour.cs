using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    /**
     * Public Fields
    **/
    public GameObject Holder;

    /**
     * Private Fields
    **/
    private float nextAttackTime;

    private float nextBigAttackTime;

    /**
     * Private Serialize Fields
    **/
    [SerializeField]
    private float attackCoolDownDuration;

    [SerializeField]
    private float bigAttackCoolDownDuration;


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
                //Damage and launch attack countdown
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
                //Damage and launch big attack countdown
                other.gameObject.GetComponent<LivingEntity>().Damage(2);
                nextBigAttackTime = Time.time + bigAttackCoolDownDuration;
            }
        }

        //IF ENEMY ATTACKS
        if (Holder.CompareTag("Enemy"))
        {
            //If collide with player and can attack
            if (other.gameObject.CompareTag("Player") && other.isTrigger == false && !other.gameObject.GetComponent<Player>().IsBlocking && nextAttackTime < Time.time)
            {
                //Attacks
                var _EnemyDetectionAttack = GetComponentInParent<EnemyDetectionAttack>();
                var _EnemyAttackRange = GetComponentInParent<EnemyAttackRange>();
                if (_EnemyDetectionAttack != null && _EnemyAttackRange == null)
                {
                    _EnemyDetectionAttack.AttackAnimation();
                    StartCoroutine(AnimationAxeMan(other.gameObject));
                }
                else
                {
                    nextAttackTime = Time.time + attackCoolDownDuration;
                    other.gameObject.GetComponent<LivingEntity>().Damage(1);
                    AudioManager.Instance.Play("Axe man attack");
                }
            }
        }
    }

    /// <summary>
    ///     Wait for attack animation
    /// </summary>
    private IEnumerator AnimationAxeMan(GameObject otherGameObject)
    {
        if (otherGameObject != null)
        {
            nextAttackTime = Time.time + attackCoolDownDuration;
            yield return new WaitForSeconds(.5f);
            otherGameObject.GetComponent<LivingEntity>().Damage(1);
        }
    }

    /// <summary>
    ///     Tells if big attack can be used
    /// </summary>
    public bool IsBigAttackReset()
    {
        return nextBigAttackTime < Time.time;
    }

    /// <summary>
    ///     Tells if attack can be used
    /// </summary>
    public bool IsAttackReset()
    {
        return nextAttackTime < Time.time;
    }
}