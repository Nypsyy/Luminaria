using UnityEngine;

/* This object updates the inventory UI. */

public class EquipmentUI : MonoBehaviour
{
	public Transform equiParent;
	public GameObject characterUI;

	EquipmentManager equipmentManager;    // Our current equipment


	void Start()
	{
		equipmentManager = EquipmentManager.instance;
		equipmentManager.onEquipmentChangedCallback += EquipUpdateUI;    // Subscribe to the onItemChanged callback
	}

	void Update()
	{
		

		if (Input.GetButtonDown("Character"))
		{
			characterUI.SetActive(!characterUI.activeSelf);
			EquipUpdateUI();
		}
	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	void EquipUpdateUI()
	{
		EquipSlot[] equipSlots = GetComponentsInChildren<EquipSlot>();

		for (int i = 0; i < equipSlots.Length; i++)
		{
			if (equipmentManager.currentEquipment[i] != null)  // If there is an item to add
			{
				equipSlots[i].AddItem(equipmentManager.currentEquipment[i]);   // Add it
			}
			else
			{
				// Otherwise clear the slot
				equipSlots[i].ClearSlot();
			}
		}
	}


}
