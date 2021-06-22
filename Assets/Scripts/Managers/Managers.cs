using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This class make sure that we can only have one instance of Managers (SingletonPatter)
public class Managers : MonoBehaviour
{
    #region Singleton

    /// The unique instance of Managers
    private static Managers _Instance;

    /// <summary>
    ///     Singleton Pattern
    /// </summary>
    private void Awake()
    {
        if (!_Instance)
        {
            /// Store the instance if not exists
            _Instance = this;

            /// Don't destroy this instance when loading a new Scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            /// If another object already exists, we can remove the other one
            Destroy(gameObject);
        }
    }

    #endregion Singleton
}