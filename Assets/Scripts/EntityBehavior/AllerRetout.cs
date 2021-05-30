using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllerRetout : MonoBehaviour
{
    public float begin;

    public float dist = 5;

    public float speed = 5;

    public int dir;

    private Transform _Transform;

    private void Start()
    {
        _Transform = GetComponent<Transform>();
        begin = _Transform.position.x;
        dir = 1;
    }

    private void Update()
    {
        // you should'nt need a non-kinetic rigidbody attached for this simple movement!!!
        if (_Transform.position.x > begin + dist) { dir = -1; }
        else { if (transform.position.x < begin - dist) { dir = 1; } }

        _Transform.position = new Vector3(_Transform.position.x + Time.deltaTime * speed * dir,
                                          _Transform.position.y,
                                          _Transform.position.z);
    }
}