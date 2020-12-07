using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminaria;

[CreateAssetMenu(fileName = "EnnemyData", menuName = "Luminaria/Ennemy Data")]
public class EnnemyData : ScriptableObject
{
    public string ennemyName;
    public GameObject ennemyModel;
    public float health = 100;
    public float speed = 1.5f;
    public float damage = 1f;
    public Element element;
}
