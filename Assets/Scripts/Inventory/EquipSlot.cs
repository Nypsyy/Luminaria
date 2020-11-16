using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
	public Image icon;          // Reference to the Icon image
	public Button removeButton; // Reference to the remove button

	Item equip;

	// Add item to the slot
	public void AddItem(Item newEquip)
	{

		equip = newEquip;

		icon.sprite = equip.icon;
		icon.enabled = true;
		removeButton.interactable = true;

	}

	// Clear the slot
	public void ClearSlot()
	{
		equip = null;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	public void OnRemoveButton()
	{
		EquipmentManager.instance.Unequip((int)equip.equipSlot);
	}

	public void UseItem()
	{
		if (equip != null)
		{
			equip.Use();
		}
	}
}
