using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour
{
	public Transform itemsParent;   // The parent object of all the items
	public GameObject inventoryUI;  // The entire UI

	Inventory inventory;    // Our current inventory


	void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback
	}

	void Update()
	{
		if (Input.GetButtonDown("Inventory"))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
			UpdateUI();
		}
	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
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