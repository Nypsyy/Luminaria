using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion 

    public Item[] currentEquipment;

    Inventory inventory;

    public delegate void OnEquipmentChangedCallback();
    public OnEquipmentChangedCallback onEquipmentChangedCallback;

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(Item.EquipmentSlot)).Length;
        currentEquipment = new Item[numSlots];

    }

    public void Equip(Item newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Debug.Log(slotIndex);

        Item oldItem = null;

        
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if(onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke();
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Item oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            
        }

        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke();
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }

        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke();
        }
    }

    

}
