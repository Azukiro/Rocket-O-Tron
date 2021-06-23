using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationEvents : MonoBehaviour
{
    /**
     * Functions called by dragon animation events
    **/

    /// <summary>
    ///     Destroy dragon gameobject for dragon animation events
    /// </summary>
    public void DestroyFatherFunction()
    {
        Destroy(transform.parent.gameObject);
    }

    /// <summary>
    ///     Play sound for dragon animation event
    /// </summary>
    public void PlaySound(string soundName)
    {
        AudioManager.Instance.Play(soundName);
    }
}
