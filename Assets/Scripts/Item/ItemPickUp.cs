using UnityEngine;

public class ItemPickUp : Interactable
{

    public Item item;   // Item to put in the inventory on pickup
    public GameObject itemUI;

    // When the player interacts with the items
    public override void Interact()
    {
        base.Interact();

        PickUp();   // Pick it up!
    }

    // Pick up the item
    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);    // Add to inventory

        // If successfully picked up
        if (wasPickedUp)
        {
            item.isPickedUp = true;
            itemUI.SetActive(false);
        }
    }


}
