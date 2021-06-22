using UnityEngine;
using System.Collections;

public class Util
{
    public delegate void VoidCallback();

    // Warning : Don't forget to use StartCoroutine(...)
    public static IEnumerator ExecuteAfterTime(float time, VoidCallback callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}