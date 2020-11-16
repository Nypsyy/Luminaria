using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public List<Item> items;
    public Item[] currentEquipment;

    public PlayerData(Inventory inventory, EquipmentManager equipmentManager)
    {
        items = inventory.items;
        currentEquipment = equipmentManager.currentEquipment;
    }
}
