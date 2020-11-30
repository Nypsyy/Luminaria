using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region Singleton

    public static Shop instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Shop found! Not good :o");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemBought();
    public OnItemBought onItemBoughtCallback;
    
    public int space = 10;
    public List<Item> shopItems = new List<Item>();

    public bool Add(Item item)
    {
        if (shopItems.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }

        shopItems.Add(item);

        if (onItemBoughtCallback != null)
        {
            onItemBoughtCallback.Invoke();
        }
        return true;
    }
    
    public void Remove(Item item)
    {
        shopItems.Remove(item);

        if (onItemBoughtCallback != null)
            onItemBoughtCallback.Invoke();
    }
}
