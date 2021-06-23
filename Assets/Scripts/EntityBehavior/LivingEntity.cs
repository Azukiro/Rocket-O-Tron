using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using SDD.Events;

public class LivingEntity : MonoBehaviour
{
    /**
     * Private Serialize Fields
    **/
    [SerializeField] private float lives = 3;
    [SerializeField] private float currentLives;
    [SerializeField] private GameObject lifebar;

    /**
     * Functions
    **/

    private void Start()
    {
        //Initialize lives and lifebar
        currentLives = lives;
        lifebar.transform.localScale = new Vector3(0.1f, 0.1f, currentLives / lives);
    }

    /// <summary>
    ///     Damage living enity
    /// </summary>
    public void Damage(float attackDamages, [CallerMemberName] string callerName = "")
    {
        //Update lives and lifebar
        currentLives -= attackDamages;
        DrawLifeBar(true);

        //If the living entity is a player
        if (gameObject.CompareTag("Player"))
        {
            //Launch event and sound
            EventManager.Instance.Raise(new GamePlayerLooseLifeEvent() { _ELife = (int)currentLives });
            AudioManager.Instance.Play("User loose life");
        }
        else if (gameObject.CompareTag("Enemy") && currentLives <= 0)
            //If enemy, luanch event
            EventManager.Instance.Raise(new GamePlayerKillEnemyEvent());

        //If no more lives
        if (currentLives <= 0)
        {
            //Update lives and kill entity
            currentLives = 0;
            Kill();

            //Launch sounds
            if (gameObject.CompareTag("Player"))
            {
                AudioManager.Instance.Play("User die");
            }
            if (gameObject.CompareTag("Enemy"))
            {
                AudioManager.Instance.Play("Axe man die");
            }
        }
    }

    /// <summary>
    ///     Heal the living entity
    /// </summary>
    public void Heal(float healing)
    {
        //Update lives and lifebar
        currentLives = currentLives + healing;
        if (currentLives > lives)
            currentLives = lives;
        DrawLifeBar(false);

        //Play sound
        AudioManager.Instance.Play("User catch potion");
    }

    /// <summary>
    ///     Update lifebar after life update
    /// </summary>
    private void DrawLifeBar(bool damage)
    {
        if (damage)
            lifebar.transform.localScale = new Vector3(0.1f, 0.1f, lifebar.transform.localScale.z * currentLives / lives);
        else
            lifebar.transform.localScale = new Vector3(0.1f, 0.1f, currentLives / lives);
    }

    /// <summary>
    ///     "Kill" the entity
    /// </summary>
    private void Kill()
    {
        //If player, detach camera before death
        if (gameObject.CompareTag("Player"))
            transform.GetComponentInChildren<Camera>().transform.SetParent(null);
        Destroy(gameObject);
    }
}