using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string abilityName;
    public Sprite abilitySprite;

    [TextArea(1, 3)]
    public string abilityDesc;
    public Ability[] previousAbility;
    public int abilityLevel;
}
