using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private IEnumerator _rotationCoroutine;

    private void Start()
    {
    }

    private void StartSwordRotation()
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

    private void StopSwordRotation()
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

    // Cette coroutine effectue une rotation infinie de l'épée
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

    private void OnGUI()
    {
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