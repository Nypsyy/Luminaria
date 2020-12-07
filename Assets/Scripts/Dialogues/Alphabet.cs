using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabet : MonoBehaviour
{
    #region Singleton

    public static Alphabet instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Alphabet found! Not good :o");
            return;
        }

        instance = this;
    }

    #endregion

    public struct SpriteData
    {
        public Sprite sprite;
        public float width;
    }

    char[] chars = "xbdhiklacefgjmnopqrstuvwyz$ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789(*!?}^)#{%&+@></-.:,;".ToCharArray();
    SpriteData[] spriteDatas = new SpriteData[84];
    public Dictionary<char, SpriteData> inventoryNameDictionary = new Dictionary<char, SpriteData>();

    Sprite[] spriteSheet;

    private void Start()
    {
        spriteSheet = Resources.LoadAll<Sprite>("BetterPixels");

        for (int i = 0; i < 84; i++)
        {
            spriteDatas[i].sprite = spriteSheet[i];
            spriteDatas[i].width = 0.3f;
        }

        if (spriteDatas != null)
        {
            for (int i = 0; i < spriteDatas.Length; i++)
            {
                inventoryNameDictionary.Add(chars[i], spriteDatas[i]);
            }
        }
    }

    public void GetWidth()
    {

    }

}
