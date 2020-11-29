using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public Ability[] abilities;
    public AbilityButton[] abilityButtons;

    public Ability activateButton;

    public int abilityMaxLevel = 15;
    public int abilityCurrentLevel = 0;

    public Text textLevel;

    GameObject children;
    TextMeshProUGUI text;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            AbilityManager.instance.abilities[i].abilityLevel = 0;
            children = AbilityManager.instance.abilities[i].transform.Find("abilityLevel").gameObject;

            if (children != null)
            {
                text = children.GetComponent<TextMeshProUGUI>();
                text.text = AbilityManager.instance.abilities[i].abilityLevel.ToString();
            }

        }
    }

}
