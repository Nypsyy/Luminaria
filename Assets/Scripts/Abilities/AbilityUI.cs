using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminaria;

public class AbilityUI : MonoBehaviour
{
    public Transform itemsParent;   // The parent object of all the items
    public GameObject abilityUI;  // The entire UI

    Inventory inventory;    // Our current inventory
    private GamemodeManager gmm;

    void Update()
    {
        /*if (gmm.state == Gamemode.INVENTORY_OPEN)
        {
            abilityUI.SetActive(!abilityUI.activeSelf);
        }*/

        if (Input.GetKeyDown(KeyCode.S))
        {
            abilityUI.SetActive(!abilityUI.activeSelf);
        }
    }
}
