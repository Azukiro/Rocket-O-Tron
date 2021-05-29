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
     *      Print debug buttons
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
        // Empêcher la duplication de coroutines
        if (_rotationCoroutine != null)
        {
            Debug.Log("[StartSwordRotation] Coroutine is already started!");
            return;
        }

        // Stocker la coroutine pour pouvoir l'interrompre par la suite
        _rotationCoroutine = RotateSwordCoroutine();

        // Lancer la coroutine de rotation
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
        // On ne fait rien si la coroutine n'est pas lancée
        if (_rotationCoroutine == null)
        {
            Debug.Log("[StopSwordRotation] Coroutine does not exists!");
            return;
        }

        // Interrompre la coroutine de rotation
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
            // Effectuer une rotation de l'épée
            transform.Rotate(Vector3.right, Time.deltaTime * 500);

            // Attendre la prochaine frame
            yield return null;
        }
    }

    /**
     * <summary>
     *      Print debug buttons
     * </summary>
     *
     * <author>
     *      Ewen BOUQUET
     * </author>
     **/

    private void OnGUI()
    {
        if (!debug)
            return;

        if (GUI.Button(new Rect(10, 110, 250, 50), "START Rotation"))
        {
            StartSwordRotation();
        }

        if (GUI.Button(new Rect(10, 210, 250, 50), "STOP Rotation"))
        {
            StopSwordRotation();
        }
    }
}