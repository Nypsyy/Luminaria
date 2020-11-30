using UnityEngine;
using UnityEngine.EventSystems;

/* The base item class. All items should derive from this. */

[System.Serializable]
public class Item
{

	public string name = "New Item";    // Name of the item
	public Sprite icon = null;              // Item icon
	public bool isPickedUp = false;      // Is the item default wear?
	public ItemType itemType;

	public EquipmentSlot equipSlot;
	public int defenseModifier;
	public int damageModifier;

	public int price;

	public enum ItemType
    {
		Equipment,
		Consumable,
    }
	public enum EquipmentSlot
	{
		Head,
		Chest,
		Legs,
		LeftHand,
		RightHand,
	}

	public virtual void Use()
    {
		if(itemType == ItemType.Equipment)
        {
			EquipmentManager.instance.Equip(this);
			RemoveFromInventory();
		}
	}

	public void RemoveFromInventory()
	{
		Inventory.instance.Remove(this);
	}
	public virtual void Buy()
    {
		if (price <= Inventory.instance.GetCurrency())
		{
			Inventory.instance.Add(this);
			RemoveFromShop();
		}
    }

	public virtual void RemoveFromShop()
    {
		Shop.instance.Remove(this);
    }
}
