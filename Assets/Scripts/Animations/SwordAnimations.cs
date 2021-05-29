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
    private readonly bool _debug = true;

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
     *      Particle system
     * </summary>
     *
     * <author>
     *      Ewen BOUQUET
     * </author>
     **/
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    /**
     * <summary>
     *      Start the sword rotation and the particle system
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

        // Launch the coroutine
        StartCoroutine(_rotationCoroutine);

        // Launch the particule system
        _particleSystem.Play();
    }

    /**
     * <summary>
     *      Stop the sword rotatation and the particle system
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

        // Stop the particule system
        _particleSystem.Stop();
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
        if (!_debug) return;

        // Display the start rotation button
        if (GUI.Button(new Rect(10, 110, 250, 50), "START Rotation"))
            StartSwordRotation();

        // Display the stop rotation button
        if (GUI.Button(new Rect(10, 210, 250, 50), "STOP Rotation"))
            StopSwordRotation();
    }
}