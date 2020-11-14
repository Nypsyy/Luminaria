﻿using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

	new public string name = "New Item";    // Name of the item
	public Sprite icon = null;              // Item icon
	public bool isDefaultItem = false;      // Is the item default wear?

	public virtual void Use()
    {

    }

	public void RemoveFromInventory()
	{
		Inventory.instance.Remove(this);
	}
}