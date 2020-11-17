using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

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
    [SerializeField] private int playerId = 0;

    private Player player;
    private Inventory inventory;

    public delegate void OnEquipmentChangedCallback();
    public OnEquipmentChangedCallback onEquipmentChangedCallback;

    private bool unequipAll;

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(Item.EquipmentSlot)).Length;
        currentEquipment = new Item[numSlots];
    }

    void Update()
    {
        GetInputs();

        if (unequipAll)
        {
            UnequipAll();
        }

        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke();
        }
    }

    private void GetInputs()
    {
        unequipAll = player.GetButtonDown("Unequip All");
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

        if (onEquipmentChangedCallback != null)
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
        Debug.Log("UNEQUIPPING");
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

    }
}
