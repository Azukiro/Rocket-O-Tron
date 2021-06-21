using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using SDD.Events;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] private float lives = 3;
    [SerializeField] private float currentLives;
    [SerializeField] private GameObject lifebar;

    // Start is called before the first frame update
    private void Start()
    {
        currentLives = lives;
        lifebar.transform.localScale = new Vector3(0.1f, 0.1f, currentLives / 3);
    }

    public void Damage(float attackDamages, [CallerMemberName] string callerName = "")
    {
        currentLives -= attackDamages;
        DrawLifeBar(true);

        if (gameObject.CompareTag("Player"))
            EventManager.Instance.Raise(new GamePlayerLooseLifeEvent() { eLife = (int)currentLives });
        else if (gameObject.CompareTag("Enemy") && currentLives <= 0)
            EventManager.Instance.Raise(new GamePlayerKillEnnemyEvent());

        if (currentLives <= 0)
            Kill();
    }

    public void Heal(float healing)
    {
        currentLives = currentLives + healing;
        if (currentLives > lives)
            currentLives = lives;
        DrawLifeBar(false);
    }

    private void DrawLifeBar(bool damage)
    {
        if (damage)
            lifebar.transform.localScale = new Vector3(0.1f, 0.1f, lifebar.transform.localScale.z * currentLives / lives);
        else
            lifebar.transform.localScale = new Vector3(0.1f, 0.1f, currentLives / lives);
    }

    private void Kill()
    {
        if (gameObject.CompareTag("Player"))
            transform.GetComponentInChildren<Camera>().transform.SetParent(null);
        Destroy(gameObject);
    }
}