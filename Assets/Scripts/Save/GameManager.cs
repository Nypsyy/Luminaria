using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Inventory inventory;
    EquipmentManager equipmentManager;
    [SerializeField] GameObject pauseMenu;

    void Start()
    {
        inventory = Inventory.instance;
        equipmentManager = EquipmentManager.instance;
    }

    void Update()
    {
        if (PlayerInputs.instance.openMenu)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(inventory, equipmentManager);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        inventory.items = data.items;

        equipmentManager.currentEquipment = data.currentEquipment;
    }
}
