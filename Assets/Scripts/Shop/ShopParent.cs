using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShopParent : MonoBehaviour
{
    public List<ItemPickUp> itemPickUps = new List<ItemPickUp>();
    public GameObject shopUI;
    public Dialogues dialogues;

    List<Item> items = new List<Item>();

    bool playerNearby;
    bool isShoploaded;

    void Start()
    {
        pickUpsToItem();
    }

    void Update()
    {
        if (dialogues.openShop)
        {
            isShoploaded = false;
            PlayerInputs.instance.UpdateControllerMap("Shop");
            dialogues.openShop = false;
            shopUI.SetActive(true);
            if (!isShoploaded)
            {
                Debug.Log("load shop");
                loadShop();
            }

            if (Shop.instance.onItemBoughtCallback != null)
            {
                Debug.Log("callback");
                Shop.instance.onItemBoughtCallback.Invoke();
            }
        }

        if (PlayerInputs.instance.closeShop)
        {
            PlayerInputs.instance.UpdateControllerMap("World Exploration");
            shopUI.SetActive(false);
        }
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
