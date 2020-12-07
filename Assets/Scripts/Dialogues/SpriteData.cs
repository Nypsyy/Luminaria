using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues", order = 51)]
public class SpriteData : ScriptableObject
{
    [System.Serializable]
    public class ResponseOptions
    {
        public SpriteData spriteData;
        public string text;

        public ResponseOptions(SpriteData sprite, string txt)
        {
            spriteData = sprite;
            text = txt;
        }

        public SpriteData GetSpriteData()
        {
            return spriteData;
        }
        
        public string GetText()
        {
            return text;
        }
    }

    [SerializeField] int dialogueID;

    [TextArea]
    [SerializeField] List<string> dialogue = new List<string>();
    
    [SerializeField] List<ResponseOptions> responseOptions = new List<ResponseOptions>();

    public List<string> GetDialogue()
    {
        return dialogue;
    }

    public List<ResponseOptions> GetResponse()
    {
        return responseOptions;
    }

    public int GetID()
    {
        return dialogueID;
    }
}
