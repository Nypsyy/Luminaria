﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class MenuButtonController : MonoBehaviour
{
    public struct MenuInput
    {
        public bool select;
        public bool returnBack;
        public bool up;
        public bool left;
        public bool down;
        public bool right;
    }

    // Use this for initialization
    public int index;
    public AudioSource buttonSounds;

    [SerializeField] int maxIndex;
    [SerializeField] int playerId = 0;
    [SerializeField] string scene;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] Animator musicTransition;

    private Player player;
    public MenuInput menu;
    public Mouse mouse;


    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        mouse = player.controllers.Mouse;

        foreach (ControllerMapEnabler.RuleSet rs in player.controllers.maps.mapEnabler.ruleSets)
            rs.enabled = false;

        player.controllers.maps.mapEnabler.ruleSets.Find(item => item.tag == "Menu").enabled = true;
        player.controllers.maps.mapEnabler.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        ProcessInputs();
    }

    private void GetInputs()
    {
        menu.up = player.GetButtonDown("Menu Up");
        menu.left = player.GetButtonDown("Menu Left");
        menu.down = player.GetButtonDown("Menu Down");
        menu.right = player.GetButtonDown("Menu Right");
        menu.select = player.GetButtonDown("Select");
        menu.returnBack = player.GetButtonDown("Return");
    }

    private void ProcessInputs()
    {
        if (menu.up)
            index--;
        else if (menu.down)
            index++;

        if (index > maxIndex)
            index = 0;
        else if (index < 0)
            index = maxIndex;

        if (menu.select)
            switch (index)
            {
                case 0:
                    levelLoader.LoadNextLevel();
                    musicTransition.SetTrigger("Fade");
                    return;
                case 1:
                    return;
                case 2:
                    return;
                case 3:
                    Application.Quit();
                    return;
            }
    }
}
