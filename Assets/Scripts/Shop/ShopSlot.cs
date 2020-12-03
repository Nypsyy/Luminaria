using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;

    Item item;

    void Update()
    {
        if(item != null) text.text = item.price.ToString();
    }
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void BuyItem()
    {
        if (item != null)
        {
            item.Buy();
            text.text = "";
        }
    }
}
