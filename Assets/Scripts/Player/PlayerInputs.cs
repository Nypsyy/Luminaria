using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Luminaria;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] int playerId;

    [HideInInspector] public Mouse mouse;

    [HideInInspector] public bool menuSelect;
    [HideInInspector] public bool menuBack;
    [HideInInspector] public bool menuUp;
    [HideInInspector] public bool menuLeft;
    [HideInInspector] public bool menuDown;
    [HideInInspector] public bool menuRight;

    [HideInInspector] public bool openInventory;
    [HideInInspector] public bool closeUI;
    [HideInInspector] public bool closeShop;

    [HideInInspector] public float moveHorizontal;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool interract;
    [HideInInspector] public bool openMenu;
    [HideInInspector] public bool openWheel;
    [HideInInspector] public bool closeWheel;

    [HideInInspector] public bool castSpell;
    [HideInInspector] public bool summonSpell;
    [HideInInspector] public bool releaseCast;

    Player player;
    ControllerMapEnabler controllerMapEnabler;

    #region Singleton

    public static PlayerInputs instance;
    void Awake()
    {
        if (instance != null) return;
        instance = this;

        player = ReInput.players.GetPlayer(playerId);
        mouse = ReInput.controllers.Mouse;
        controllerMapEnabler = player.controllers.maps.mapEnabler;
    }

    #endregion

    void Update()
    {
        GetInputs();
        ProcessInputs();
    }

    void GetInputs()
    {
        menuSelect = player.GetButtonDown("Select Menu");
        menuBack = player.GetButtonDown("Return");
        menuUp = player.GetButtonDown("Menu Up");
        menuLeft = player.GetButtonDown("Menu Left");
        menuDown = player.GetButtonDown("Menu Down");
        menuRight = player.GetButtonDown("Menu Right");

        openInventory = player.GetButtonDown("Open Inventory");
        closeUI = player.GetButtonDown("Close UI");
        closeShop = player.GetButtonDown("Close Shop");

        jump = player.GetButtonDown("Jump");
        moveHorizontal = player.GetAxisRaw("Move Horizontal");
        interract = player.GetButtonDown("Interract");
        openMenu = player.GetButtonDown("Open Menu");
        openWheel = player.GetButton("Open Wheel");
        closeWheel = player.GetButtonUp("Open Wheel");

        castSpell = player.GetButtonDown("Cast Spell");
        summonSpell = player.GetButtonLongPressDown("Cast Spell");
        releaseCast = player.GetButtonUp("Cast Spell");
    }

    void ProcessInputs()
    {
        if (openMenu)
        {
            UpdateControllerMap("Menu");
            GamemodeManager.instance.state = Gamemode.MENU;
        }
        else if (menuBack)
        {
            UpdateControllerMap("World Exploration");
            GamemodeManager.instance.state = Gamemode.WORLD_EXPLORATION;
        }
        if (openInventory)
        {
            UpdateControllerMap("Inventory");
            GamemodeManager.instance.state = Gamemode.INVENTORY_OPEN;
        }
        if (closeUI)
        {
            UpdateControllerMap("World Exploration");
            GamemodeManager.instance.state = Gamemode.WORLD_EXPLORATION;
        }
    }

    // Must be called by classes that has input attribute
    public void UpdateControllerMap(string rsTag)
    {
        foreach (ControllerMapEnabler.RuleSet rs in controllerMapEnabler.ruleSets)
            rs.enabled = false;
        controllerMapEnabler.ruleSets.Find(item => item.tag == rsTag).enabled = true;
        controllerMapEnabler.Apply();
    }
}
