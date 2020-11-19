using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminaria;

public class GamemodeManager : MonoBehaviour
{
    #region Singleton

    public static GamemodeManager instance;
    void Awake()
    {
        if (instance != null) return;
        instance = this;

        state = Gamemode.WORLD_EXPLORATION;
    }

    #endregion 

    public Gamemode state;
}
