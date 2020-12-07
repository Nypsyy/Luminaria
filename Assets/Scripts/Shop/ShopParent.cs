using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShopParent : MonoBehaviour
{
    public List<ItemPickUp> itemPickUps = new List<ItemPickUp>();
    public GameObject shopUI;
    public Dialogues dialogues;

    public List<Item> items = new List<Item>();

    bool playerNearby;
    bool isShoploaded;

    void Start()
    {
        pickUpsToItem();
    }

    void Update()
    {
        if ((PlayerInputs.instance.interract && playerNearby) || dialogues.openShop && playerNearby)
        {
            Debug.Log("C'est ouvert");
            dialogues.openShop = false;
            PlayerInputs.instance.UpdateControllerMap("Inventory");
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
            PlayerInputs.instance.UpdateControllerMap("World Exploration");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerNearby = false;
        isShoploaded = false;
    }


}
