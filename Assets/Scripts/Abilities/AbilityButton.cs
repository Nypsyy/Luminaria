using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityButton : MonoBehaviour
{
    public Text abilityNameText;
    public Text abilityDescText;
    public Image abilityImage;

    public Button upgradeButton;

    GameObject children;
    TextMeshProUGUI text;


    public int buttonId;

    public void Start()
    {
        abilityNameText.text = "";
        abilityDescText.text = "";
        abilityImage.sprite = null;

        
    }

    public void PressAbilityButton()
    {
        AbilityManager.instance.activateButton = transform.GetComponent<Ability>();
        children = transform.Find("abilityLevel").gameObject;

        if(children != null)
        {
            text = children.GetComponent<TextMeshProUGUI>();
            text.text = AbilityManager.instance.abilities[buttonId].abilityLevel.ToString();
        }

        abilityNameText.text = AbilityManager.instance.abilities[buttonId].abilityName;
        abilityDescText.text = AbilityManager.instance.abilities[buttonId].abilityDesc;
        abilityImage.sprite = AbilityManager.instance.abilities[buttonId].abilitySprite;
    }

    public void onUpgradeButton()
    {
        AbilityManager.instance.activateButton = transform.GetComponent<Ability>();
        children = transform.Find("abilityLevel").gameObject;

        if (children != null)
        {
            AbilityManager.instance.abilities[buttonId].abilityLevel += 1;

            text = children.GetComponent<TextMeshProUGUI>();
            text.text = AbilityManager.instance.abilities[buttonId].abilityLevel.ToString();
        }
    }
   
}
