using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationEvents : MonoBehaviour
{
    public void DestroyFatherFunction()
    {
        Destroy(transform.parent.gameObject);
    }

    public void PlaySound(string soundName)
    {
        AudioManager.Instance.Play(soundName);
    }
}
