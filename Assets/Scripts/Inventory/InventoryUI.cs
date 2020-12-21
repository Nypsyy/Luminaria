using UnityEngine;
using Luminaria;
using TMPro;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;   // The parent object of all the items
    public GameObject inventoryUI;  // The entire UI
    public TextMeshProUGUI text;

    Inventory inventory;    // Our current inventory
    private GamemodeManager gmm;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback
    }

    void Update()
    {
        if (PlayerInputs.instance.openInventory)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUI();
        }

        if(PlayerInputs.instance.closeUI)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUI();

        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.AddCurrency(50);
        }

        text.text = inventory.GetCurrency().ToString();
    }

    void UpdateUI()
    {
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();

        // Loop through all the slots
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)  // If there is an item to add
            {
                slots[i].AddItem(inventory.items[i]);   // Add it
            }
            else
            {
                // Otherwise clear the slot
                slots[i].ClearSlot();
            }
        }
    }
}