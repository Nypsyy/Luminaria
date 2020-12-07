using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ElementWheelBehavior : MonoBehaviour
{
    Mouse mouse;
    Vector2 normalizedMousePos;
    float currentMouseAngle;

    public int selection;
    int prevSelection;

    [SerializeField] GameObject wheel;
    [SerializeField] GameObject[] items;
    ElementWheelItemBehavior currentItem;
    ElementWheelItemBehavior prevItem;

    void Start()
    {
        wheel.SetActive(false);
        mouse = ReInput.controllers.Mouse;
    }

    void Update()
    {
        if (PlayerInputs.instance.openWheel)
        {
            wheel.SetActive(true);

            normalizedMousePos = new Vector2(mouse.screenPosition.x - Screen.width / 2, mouse.screenPosition.y - Screen.height / 2);
            currentMouseAngle = Mathf.Atan2(normalizedMousePos.y, normalizedMousePos.x) * Mathf.Rad2Deg;
            currentMouseAngle = (currentMouseAngle + 360) % 360;
            selection = (int)currentMouseAngle / 90;

            if (selection != prevSelection)
            {

                prevItem = items[prevSelection].GetComponent<ElementWheelItemBehavior>();
                prevItem.Deselect();

                currentItem = items[selection].GetComponent<ElementWheelItemBehavior>();
                currentItem.Select();

                prevSelection = selection;
            }
        }
        else if (PlayerInputs.instance.closeWheel)
        {
            currentItem.ResetUI();
            wheel.SetActive(false);
        }
    }
}
