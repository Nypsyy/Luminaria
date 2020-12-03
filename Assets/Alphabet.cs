using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabet : MonoBehaviour
{
    public struct SpriteData
    {
        Sprite sprite;
        int width;
    }

    char[] chars = "!€#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[§]^_²abcedfghijklmnopqrstuvwxyz{|}".ToCharArray();
    Sprite[] spriteSheetSprites;
    SpriteData[] spriteDatas;

    public Dictionary<char, Sprite> inventoryNameDictionary = new Dictionary<char, Sprite>();

    
    

    private void Start()
    {
        spriteSheetSprites = Resources.LoadAll<Sprite>("font-1");

        for (int i = 0; i < spriteSheetSprites.Length; i++)
        {
            //spriteDatas[i].
        }

        if (spriteSheetSprites != null)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                inventoryNameDictionary.Add(chars[i], spriteSheetSprites[i]);
            }
        }
    }

    public void GetWidth()
    {

    }

}
