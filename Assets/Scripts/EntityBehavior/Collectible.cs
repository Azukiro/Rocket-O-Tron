using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private GameObject Gfx;
    private float itemRotationSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gfx.transform.Rotate(new Vector3(0,itemRotationSpeed*Time.deltaTime,0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LivingEntity>().Heal(2f);
            Destroy(gameObject);
        }
    }
}
