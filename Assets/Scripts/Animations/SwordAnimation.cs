using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation
{
    #region Singleton pattern

    private SwordAnimation()
    {
    }

    private static SwordAnimation instance;

    public static SwordAnimation Instance
    {
        get
        {
            if (instance == null)
                instance = new SwordAnimation();
            return instance;
        }
    }

    #endregion Singleton pattern

    #region Animation methods

    public void rotate()
    {
    }

    #endregion Animation methods
}