using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 *      Manage the sword animations
 * </summary>
 *
 * <author>
 *      Ewen BOUQUET
 * </author>
 **/

public class SwordAnimations : MonoBehaviour
{
    /**
     * <summary>
     *      Display debug buttons
     * </summary>
     *
     * <author>
     *      Ewen BOUQUET
     * </author>
     **/
    private bool debug = true;

    /**
     * <summary>
     *      Sword rotation coroutine
     * </summary>
     *
     * <author>
     *      Ewen BOUQUET
     * </author>
     **/
    private IEnumerator _rotationCoroutine = null;

    /**
     * <summary>
     *      Start the sword rotation
     * </summary>
     *
     * <author>
     *      Ewen BOUQUET
     * </author>
     **/

    public void StartSwordRotation()
    {
        // Unique coroutine
        if (_rotationCoroutine != null)
        {
            Debug.Log("[StartSwordRotation] Coroutine is already started!");
            return;
        }

        // Save the coroutine
        _rotationCoroutine = RotateSwordCoroutine();

        // Lauch the coroutine
        StartCoroutine(_rotationCoroutine);
    }

    /**
     * <summary>
     *      Stop the sword rotatation
     * </summary>
     *
     * <author>
     *      Ewen BOUQUET
     * </author>
     **/

    public void StopSwordRotation()
    {
        // Require an active coroutine
        if (_rotationCoroutine == null)
        {
            Debug.Log("[StopSwordRotation] Coroutine does not exists!");
            return;
        }

        // Stop the active coroutine
        StopCoroutine(_rotationCoroutine);
        _rotationCoroutine = null;
    }

    /**
     * <summary>
     *      Rotation coroutine
     * </summary>
     *
     * <author>
     *      Ewen BOUQUET
     * </author>
     **/

    private IEnumerator RotateSwordCoroutine()
    {
        while (true)
        {
            // Sword rotation
            transform.Rotate(Vector3.right, Time.deltaTime * 500);

            // Wait the next frame
            yield return null;
        }
    }

    /**
     * <summary>
     *      Display some debug buttons
     * </summary>
     *
     * <author>
     *      Ewen BOUQUET
     * </author>
     **/

    private void OnGUI()
    {
        // Disable on debug
        if (!debug) return;

        // Display the start rotation button
        if (GUI.Button(new Rect(10, 110, 250, 50), "START Rotation"))
            StartSwordRotation();

        // Display the stop rotation button
        if (GUI.Button(new Rect(10, 210, 250, 50), "STOP Rotation"))
            StopSwordRotation();
    }
}