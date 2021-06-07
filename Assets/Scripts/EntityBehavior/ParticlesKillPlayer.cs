using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesKillPlayer : MonoBehaviour
{
    public ParticleSystem part;

    // Start is called before the first frame update
    private void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<LivingEntity>().Damage(3);
        }
    }
}