using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesKillPlayer : MonoBehaviour
{
    /**
     * Public Fields
    **/
    public ParticleSystem part;

    /**
     * Functions
    **/

    private void Start()
    {
        //Get particle system
        part = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        //Kill player or enemies when fire particles collide with
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            LivingEntity livingComponent = other.gameObject.GetComponent<LivingEntity>();
            if(livingComponent!=null)
                livingComponent.Damage(3);
        }
    }
}