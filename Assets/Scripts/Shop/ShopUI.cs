using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public Transform shopParent;   // The parent object of all the items
    public GameObject shopUI;  // The entire UI

    Shop shop;    // Our current inventory

    void Start()
    {
        shop = Shop.instance;
        shop.onItemBoughtCallback += UpdateUI;    // Subscribe to the onItemChanged callback
    }

    void UpdateUI()
    {
        GameObject test = GameObject.Find("/Canvas/Shop/ShopParent");
        if (test != null)
        {
            ShopSlot[] slots = test.GetComponentsInChildren<ShopSlot>();
            Debug.Log(slots.Length);
            // Loop through all the slots
            for (int i = 0; i < slots.Length; i++)
            {

                if (i < shop.shopItems.Count)  // If there is an item to add
                {
                    slots[i].AddItem(shop.shopItems[i]);   // Add it
                }
                else
                {
                    // Otherwise clear the slot
                    slots[i].ClearSlot();
                }
            }
        }
    }
}
