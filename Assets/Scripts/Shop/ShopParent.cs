using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShopParent : MonoBehaviour
{
    public List<ItemPickUp> itemPickUps = new List<ItemPickUp>();
    public GameObject shopUI;

    public List<Item> items = new List<Item>();

    bool playerNearby;
    bool isShoploaded;

    void Start()
    {
        pickUpsToItem();
    }

    void Update()
    {
        if (PlayerInputs.instance.interract && playerNearby)
        {
            shopUI.SetActive(true);
            if(!isShoploaded) loadShop();

            if (Shop.instance.onItemBoughtCallback != null)
            {
                Shop.instance.onItemBoughtCallback.Invoke();
            }
            if (!isShoploaded) loadShop();
        }

        if (PlayerInputs.instance.closeUI && playerNearby)
        {
            shopUI.SetActive(false);
        }

        if (!playerNearby) shopUI.SetActive(false);
    }

    public void loadShop()
    {
        Shop.instance.shopItems = items;

        if (Shop.instance.onItemBoughtCallback != null)
        {
            Shop.instance.onItemBoughtCallback.Invoke();
        }

        isShoploaded = true;
    }

    public void pickUpsToItem()
    {
        items.Clear();

        for (int i = 0; i < itemPickUps.Count; i++)
        {
            items.Add(itemPickUps[i].item);
        }
    }


    public void OnCollisionStay2D(Collision2D collision)
    {
        playerNearby = true;

    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        playerNearby = false;
        isShoploaded = false;
    }

}
