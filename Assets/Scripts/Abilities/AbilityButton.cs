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

    bool upgradable = false;
    int temp;


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

        temp = 0;
        for (int i = 0; i < AbilityManager.instance.activateButton.previousAbility.Length; i++)
        {
            if(AbilityManager.instance.activateButton.previousAbility[i].abilityLevel >= 3)
            {
                upgradable = true;
                temp += 1;
            } else
            {
                upgradable = false;
            }
        }

        if (temp != AbilityManager.instance.activateButton.previousAbility.Length)
            upgradable = false;

        if (buttonId == 0)
            upgradable = true;

        children = transform.Find("abilityLevel").gameObject;

        if (children != null)
        {
            if (upgradable && AbilityManager.instance.abilityCurrentLevel < 15)
            {
                AbilityManager.instance.abilities[buttonId].abilityLevel += 1;
                AbilityManager.instance.abilityCurrentLevel += 1;

                AbilityManager.instance.textLevel.text = AbilityManager.instance.abilityCurrentLevel.ToString() + " / 15";

                text = children.GetComponent<TextMeshProUGUI>();
                text.text = AbilityManager.instance.abilities[buttonId].abilityLevel.ToString();
            }
        }
    }
   
}
