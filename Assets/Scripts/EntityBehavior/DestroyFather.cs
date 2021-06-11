using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFather : MonoBehaviour
{
    public void DestroyFatherFunction()
    {
        Destroy(transform.parent.gameObject);
    }
}
