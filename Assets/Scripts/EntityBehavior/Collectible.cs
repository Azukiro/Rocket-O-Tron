using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    /**
     * Private Serialize Fields
    **/

    [SerializeField] private GameObject Gfx;

    /**
     * Private Fields
    **/

    private float itemRotationSpeed = 30.0f;

    /**
     * Functions
    **/

    // Update is called once per frame
    void Update()
    {
        //Rotate collectible gfx
        Gfx.transform.Rotate(new Vector3(0,itemRotationSpeed*Time.deltaTime,0));
    }

    private void OnTriggerEnter(Collider other)
    {
        //If Player in collectible trigger
        if (other.CompareTag("Player"))
        {
            //Apply collectible effect and destroy collectible
            other.GetComponent<LivingEntity>().Heal(2f);
            Destroy(gameObject);
        }
    }
}
