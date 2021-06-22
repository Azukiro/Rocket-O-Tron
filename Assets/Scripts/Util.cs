using UnityEngine;
using System.Collections;

/// Class that propose some util static functions
public class Util
{
    /// Void callback without arguments
    public delegate void VoidCallback();

    /// <summary>
    ///     Execute a $callback function after $time seconds
    /// </summary>
    ///
    /// <remarks>
    ///     Warning : Don't forget to use StartCoroutine(...)
    /// </remarks>
    ///
    /// <param name="time">
    ///     Time to wait in float
    /// </param>
    ///
    /// <param name="callback">
    ///     Function to call
    /// </param>
    ///
    /// <returns>
    ///     An IEnumerator
    /// </returns>
    public static IEnumerator ExecuteAfterTime(float time, VoidCallback callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}