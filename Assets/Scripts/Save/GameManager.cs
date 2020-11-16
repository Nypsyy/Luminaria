using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Inventory inventory;
    EquipmentManager equipmentManager;
    
    void Start()
    {
        inventory = Inventory.instance;
        equipmentManager = EquipmentManager.instance;
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
